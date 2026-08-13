// Interop entre Blazor y Leaflet para el selector de coordenadas de Pedido.
// Cada mapa se identifica por el id del div contenedor; se guarda su instancia
// (mapa + marcador + referencia a .NET) en `mapas` para poder moverlo/destruirlo.
const mapas = {};

// Registro aparte para los mapas de doble marcador (Origen + Destino en un solo mapa).
const mapasDual = {};

function crearIconoColor(color) {
    // Pin simple en SVG (sin depender de imagenes externas) para distinguir Origen (verde)
    // de Destino (rojo) en el mismo mapa.
    const svg = `<svg xmlns="http://www.w3.org/2000/svg" width="30" height="42" viewBox="0 0 30 42">
        <path d="M15 0C6.7 0 0 6.7 0 15c0 10.5 15 27 15 27s15-16.5 15-27C30 6.7 23.3 0 15 0z" fill="${color}" stroke="#ffffff" stroke-width="1.5"/>
        <circle cx="15" cy="15" r="6" fill="#ffffff"/>
    </svg>`;
    return L.divIcon({
        html: svg,
        className: '',
        iconSize: [30, 42],
        iconAnchor: [15, 42],
        popupAnchor: [0, -40]
    });
}

const iconoOrigen = () => crearIconoColor('#16a34a');
const iconoDestino = () => crearIconoColor('#dc2626');

window.leafletInterop = {
    iniciar: function (elementId, dotNetRef, lat, lng, zoom) {
        if (mapas[elementId]) {
            window.leafletInterop.destruir(elementId);
        }

        const map = L.map(elementId).setView([lat, lng], zoom || 15);

        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        }).addTo(map);

        const marker = L.marker([lat, lng], { draggable: true }).addTo(map);

        const notificarCambio = (latlng) => {
            dotNetRef.invokeMethodAsync('OnCoordenadasSeleccionadas', latlng.lat, latlng.lng);
        };

        marker.on('dragend', function () {
            notificarCambio(marker.getLatLng());
        });

        map.on('click', function (e) {
            marker.setLatLng(e.latlng);
            notificarCambio(e.latlng);
        });

        mapas[elementId] = { map: map, marker: marker };
    },

    moverMarcador: function (elementId, lat, lng, zoom) {
        const instancia = mapas[elementId];
        if (!instancia) return;
        const latlng = [lat, lng];
        instancia.marker.setLatLng(latlng);
        instancia.map.setView(latlng, zoom || instancia.map.getZoom());
    },

    invalidarTamano: function (elementId) {
        const instancia = mapas[elementId];
        if (!instancia) return;
        setTimeout(() => instancia.map.invalidateSize(), 100);
    },

    destruir: function (elementId) {
        const instancia = mapas[elementId];
        if (!instancia) return;
        instancia.map.remove();
        delete mapas[elementId];
    },

    // ---- Mapa con dos marcadores (Origen + Destino) ----

    iniciarDual: function (elementId, dotNetRef, origenLat, origenLng, destinoLat, destinoLng, mostrarDestino, zoom) {
        if (mapasDual[elementId]) {
            window.leafletInterop.destruirDual(elementId);
        }

        const map = L.map(elementId).setView([origenLat, origenLng], zoom || 15);

        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        }).addTo(map);

        const instancia = { map: map, markerOrigen: null, markerDestino: null, activo: 'origen' };
        mapasDual[elementId] = instancia;

        const notificar = (cual, latlng) => {
            dotNetRef.invokeMethodAsync('OnCoordenadasSeleccionadas', cual, latlng.lat, latlng.lng);
        };

        instancia.markerOrigen = L.marker([origenLat, origenLng], { draggable: true, icon: iconoOrigen() }).addTo(map);
        instancia.markerOrigen.on('dragend', () => notificar('origen', instancia.markerOrigen.getLatLng()));

        if (mostrarDestino) {
            instancia.markerDestino = L.marker([destinoLat, destinoLng], { draggable: true, icon: iconoDestino() }).addTo(map);
            instancia.markerDestino.on('dragend', () => notificar('destino', instancia.markerDestino.getLatLng()));
        }

        map.on('click', function (e) {
            const cual = instancia.activo === 'destino' && instancia.markerDestino ? 'destino' : 'origen';
            const marker = cual === 'destino' ? instancia.markerDestino : instancia.markerOrigen;
            marker.setLatLng(e.latlng);
            notificar(cual, e.latlng);
        });
    },

    // Le indica al mapa cual de los dos marcadores debe moverse al hacer click (el arrastre
    // de cada marcador ya identifica cual es, independientemente de esto).
    establecerActivoDual: function (elementId, cual) {
        const instancia = mapasDual[elementId];
        if (instancia) instancia.activo = cual;
    },

    moverMarcadorDual: function (elementId, cual, lat, lng, zoom) {
        const instancia = mapasDual[elementId];
        if (!instancia) return;
        const marker = cual === 'destino' ? instancia.markerDestino : instancia.markerOrigen;
        if (!marker) return;
        marker.setLatLng([lat, lng]);
        instancia.map.setView([lat, lng], zoom || instancia.map.getZoom());
    },

    // Agrega o quita el marcador de Destino segun el checkbox "Tiene destino definido".
    mostrarOcultarDestinoDual: function (elementId, mostrar, lat, lng, dotNetRef) {
        const instancia = mapasDual[elementId];
        if (!instancia) return;

        if (mostrar && !instancia.markerDestino) {
            instancia.markerDestino = L.marker([lat, lng], { draggable: true, icon: iconoDestino() }).addTo(instancia.map);
            instancia.markerDestino.on('dragend', () => {
                dotNetRef.invokeMethodAsync('OnCoordenadasSeleccionadas', 'destino', instancia.markerDestino.getLatLng().lat, instancia.markerDestino.getLatLng().lng);
            });
        } else if (!mostrar && instancia.markerDestino) {
            instancia.map.removeLayer(instancia.markerDestino);
            instancia.markerDestino = null;
        }
    },

    invalidarTamanoDual: function (elementId) {
        const instancia = mapasDual[elementId];
        if (!instancia) return;
        setTimeout(() => instancia.map.invalidateSize(), 100);
    },

    destruirDual: function (elementId) {
        const instancia = mapasDual[elementId];
        if (!instancia) return;
        instancia.map.remove();
        delete mapasDual[elementId];
    }
};
