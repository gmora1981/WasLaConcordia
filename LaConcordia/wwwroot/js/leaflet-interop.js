// Interop entre Blazor y Leaflet para el selector de coordenadas de Pedido.
// Cada mapa se identifica por el id del div contenedor; se guarda su instancia
// (mapa + marcador + referencia a .NET) en `mapas` para poder moverlo/destruirlo.
const mapas = {};

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
    }
};
