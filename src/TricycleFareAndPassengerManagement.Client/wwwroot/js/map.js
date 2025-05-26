export function load_map() {
    // Legazpi City coordinates (latitude, longitude)
    const legazpiCoords = [13.1391, 123.7436];

    // Initialize map centered on Legazpi City with higher zoom level
    let map = L.map('map').setView(legazpiCoords, 15);

    // satellite imagery realistic view
    L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}', {
        maxZoom: 19,
        attribution: 'Tiles © Esri — Source: Esri, i-cubed, USDA, USGS, AEX, GeoEye, Getmapping, Aerogrid, IGN, IGP, UPR-EGP, and the GIS User Community'
    }).addTo(map);

    // street overlay for navigation
    const streetOverlay = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        opacity: 0.4,
        attribution: '© OpenStreetMap contributors'
    });

    // terrain/topographic overlay
    const terrainOverlay = L.tileLayer('https://stamen-tiles.a.ssl.fastly.net/terrain-lines/{z}/{x}/{y}.png', {
        maxZoom: 19,
        opacity: 0.3,
        attribution: 'Map tiles by Stamen Design, under CC BY 3.0. Data by OpenStreetMap, under ODbL.'
    });

    // realistic marker for Legazpi City center
    const customIcon = L.divIcon({
        className: 'custom-marker',
        html: `
            <div style="
                background: #ff4444;
                width: 16px;
                height: 16px;
                border-radius: 50%;
                border: 2px solid white;
                box-shadow: 0 2px 6px rgba(0, 0, 0, 0.3);
                position: relative;
            ">
                <div style="
                    position: absolute;
                    top: -8px;
                    left: -8px;
                    width: 32px;
                    height: 32px;
                    border: 2px solid #ff4444;
                    border-radius: 50%;
                    opacity: 0.3;
                    animation: ripple 2s infinite;
                "></div>
            </div>
            <style>
                @keyframes ripple {
                    0% { transform: scale(0.5); opacity: 0.5; }
                    100% { transform: scale(1.5); opacity: 0; }
                }
            </style>
        `,
        iconSize: [20, 20],
        iconAnchor: [10, 10]
    });

    L.marker(legazpiCoords, { icon: customIcon })
        .addTo(map)
        .bindPopup(`
            <div style="
                text-align: center;
                font-family: 'Roboto', sans-serif;
                padding: 12px;
                background: white;
                color: #333;
                border-radius: 8px;
                border: 1px solid #ddd;
                box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
                min-width: 200px;
            ">
                <h3 style="margin: 0 0 8px 0; font-weight: 500; color: #2c3e50;">📍 Legazpi City</h3>
                <p style="margin: 0 0 5px 0; font-size: 14px; color: #7f8c8d;">Albay Province, Bicol Region</p>
                <p style="margin: 0; font-size: 12px; color: #95a5a6;">Gateway to Mayon Volcano 🌋</p>
                <div style="margin-top: 8px; font-size: 11px; color: #bdc3c7;">
                    Population: ~209,533 | Elevation: 20m
                </div>
            </div>
        `, {
            closeButton: true,
            className: 'realistic-popup'
        });

    // Realistic notable locations in Legazpi City
    const locations = [
        {
            coords: [13.2575, 123.6856],
            name: "Mayon Volcano",
            description: "Perfect cone-shaped active volcano",
            color: "#e74c3c"
        },
        {
            coords: [13.1372, 123.7461],
            name: "Legazpi Port",
            description: "Main seaport and ferry terminal",
            color: "#3498db"
        },
        {
            coords: [13.1573, 123.7305],
            name: "Albay Park & Wildlife",
            description: "Recreational park and mini zoo",
            color: "#27ae60"
        },
        {
            coords: [13.1391, 123.7438],
            name: "Legazpi City Hall",
            description: "Municipal government center",
            color: "#9b59b6"
        },
        {
            coords: [13.1453, 123.7494],
            name: "Pacific Mall",
            description: "Shopping and entertainment center",
            color: "#f39c12"
        },
        {
            coords: [13.1443, 123.7435],
            name: "SM City Legazpi",
            description: "Modern shopping mall with various retail stores",
            color: "#1abc9c"
        },
        {
            coords: [13.1440, 123.7430],
            name: "Ayala Malls Legazpi",
            description: "Shopping center with dining and entertainment options",
            color: "#2ecc71"
        },
        {
            coords: [13.1375, 123.7390],
            name: "Yashano Mall",
            description: "Four-story shopping mall with retail and dining outlets",
            color: "#e67e22"
        },
        {
            coords: [13.1466, 123.7440],
            name: "Embarcadero de Legazpi",
            description: "Waterfront mall and lifestyle hub",
            color: "#2980b9"
        },
        {
            coords: [13.1396, 123.7430],
            name: "Legazpi Cathedral",
            description: "Cathedral of St. Gregory the Great",
            color: "#8e44ad"
        },
        {
            coords: [13.1390, 123.7432],
            name: "St. Raphael Church",
            description: "Historic church in Legazpi City",
            color: "#c0392b"
        },
        {
            coords: [13.1450, 123.7470],
            name: "Peñaranda Park",
            description: "Public park and historical site",
            color: "#16a085"
        },
        {
            coords: [13.1395, 123.7435],
            name: "Legazpi City Museum",
            description: "Museum showcasing local history and culture",
            color: "#d35400"
        },
        {
            coords: [13.1398, 123.7437],
            name: "Battle of Legazpi Monument",
            description: "Memorial commemorating the Battle of Legazpi",
            color: "#7f8c8d"
        },
        {
            coords: [13.1392, 123.7433],
            name: "Headless Monument",
            description: "Monument honoring unknown heroes",
            color: "#34495e"
        },
        {
            coords: [13.1394, 123.7436],
            name: "General Ignacio Paua Monument",
            description: "Monument dedicated to a Filipino-Chinese general",
            color: "#27ae60"
        },
        {
            coords: [13.1393, 123.7434],
            name: "Ibalong Heroes Monument",
            description: "Tribute to the heroes of the Ibalong epic",
            color: "#f1c40f"
        },
        {
            coords: [13.1397, 123.7438],
            name: "Padang Memorial Shrine",
            description: "Memorial for Typhoon Reming victims",
            color: "#e74c3c"
        },
        {
            coords: [13.1399, 123.7439],
            name: "Albay Gulf Landing Commemorative Pylon",
            description: "Monument marking the landing of Allied forces",
            color: "#9b59b6"
        },
        {
            coords: [13.1390, 123.7430],
            name: "Ligñon Hill Nature Park",
            description: "Hilltop park with panoramic views and adventure activities",
            color: "#3498db"
        },
        {
            coords: [13.1391, 123.7431],
            name: "Legazpi City Convention Center",
            description: "Venue for conferences and events",
            color: "#2ecc71"
        },
        {
            coords: [13.1392, 123.7432],
            name: "University of Santo Tomas - Legazpi",
            description: "Private Catholic university",
            color: "#e67e22"
        },
        {
            coords: [13.1393, 123.7433],
            name: "Estevez Memorial Hospital",
            description: "Medical facility in Legazpi City",
            color: "#1abc9c"
        },
        {
            coords: [13.1394, 123.7434],
            name: "Sawangan Park & Legazpi Marker",
            description: "Park with city marker and recreational space",
            color: "#8e44ad"
        },
        {
            coords: [13.1395, 123.7435],
            name: "Our Lady of Salvation Giant Statue",
            description: "Large religious statue and pilgrimage site",
            color: "#c0392b"
        },
        {
            coords: [13.1396, 123.7436],
            name: "Sumlang Lake",
            description: "Scenic lake with views of Mayon Volcano",
            color: "#16a085"
        },
        {
            coords: [13.1397, 123.7437],
            name: "Quituinan Ranch",
            description: "Ranch offering outdoor activities and accommodations",
            color: "#d35400"
        },
        {
            coords: [13.1398, 123.7438],
            name: "Farm Plate",
            description: "Farm-to-table dining experience",
            color: "#7f8c8d"
        },
        {
            coords: [13.1399, 123.7439],
            name: "Mayon Skyline View Deck",
            description: "Viewing area for Mayon Volcano",
            color: "#34495e"
        },
        {
            coords: [13.1400, 123.7440],
            name: "Japanese Tunnel",
            description: "Historical tunnel from World War II",
            color: "#27ae60"
        },
        {
            coords: [13.1401, 123.7441],
            name: "Daraga Church",
            description: "Baroque-style church with historical significance",
            color: "#f1c40f"
        },
        {
            coords: [13.1402, 123.7442],
            name: "Cagsawa Ruins",
            description: "Ruins of a church destroyed by volcanic eruption",
            color: "#e74c3c"
        },
        {
            coords: [13.1403, 123.7443],
            name: "Vera Falls",
            description: "Picturesque waterfall near Legazpi City",
            color: "#9b59b6"
        },
        {
            coords: [13.1445, 123.7352],
            name: "Bicol University - Main Campus",
            description: "Premier state university in the Bicol Region",
            color: "#2980b9"
        },
        {
            coords: [13.1463, 123.7344],
            name: "Bicol University College of Engineering",
            description: "College within BU specializing in engineering programs",
            color: "#e67e22"
        },
        {
            coords: [13.1371, 123.7442],
            name: "Bicol College",
            description: "Private educational institution in Legazpi",
            color: "#8e44ad"
        },
        {
            coords: [13.1402, 123.7446],
            name: "STI College Legazpi",
            description: "School offering ICT and business programs",
            color: "#2ecc71"
        },
        {
            coords: [13.1404, 123.7448],
            name: "AMA Computer College - Legazpi",
            description: "Information and computer technology-focused college",
            color: "#16a085"
        },
        {
            coords: [13.1392, 123.7432],
            name: "Aquinas University of Legazpi (now UST-Legazpi)",
            description: "Catholic university operated by Dominican Fathers",
            color: "#f39c12"
        },
        {
            coords: [13.1410, 123.7439],
            name: "Divine Word College of Legazpi",
            description: "Catholic college managed by SVD priests",
            color: "#9b59b6"
        },
        {
            coords: [13.1432, 123.7371],
            name: "Legazpi City Science High School",
            description: "Public science high school known for excellence",
            color: "#c0392b"
        },
        {
            coords: [13.1406, 123.7425],
            name: "Legazpi Hope Christian School",
            description: "Private Christian school in Legazpi",
            color: "#1abc9c"
        },
        {
            coords: [13.1456, 123.7364],
            name: "Bicol College of Arts and Trades (BCAT)",
            description: "Technical-vocational training institution",
            color: "#34495e"
        }
    ];

    locations.forEach(location => {
        const locationIcon = L.divIcon({
            className: 'location-marker',
            html: `
                <div style="
                    background: ${location.color};
                    width: 10px;
                    height: 10px;
                    border-radius: 50%;
                    border: 2px solid white;
                    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
                "></div>
            `,
            iconSize: [14, 14],
            iconAnchor: [7, 7]
        });

        L.marker(location.coords, { icon: locationIcon })
            .addTo(map)
            .bindPopup(`
                <div style="
                    font-family: 'Roboto', sans-serif;
                    padding: 8px;
                    background: white;
                    color: #333;
                    border-radius: 6px;
                    border: 1px solid #ddd;
                    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
                    max-width: 180px;
                ">
                    <h4 style="margin: 0 0 4px 0; font-weight: 500; color: ${location.color};">
                        ${location.name}
                    </h4>
                    <p style="margin: 0; font-size: 12px; color: #666;">
                        ${location.description}
                    </p>
                </div>
            `, {
                closeButton: false,
                className: 'realistic-popup'
            })
            .bindTooltip(location.name, {
                permanent: false,
                direction: 'top',
                className: 'realistic-tooltip'
            });
    });

    // Layer control for different map views
    const baseLayers = {
        "Satellite": L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}', {
            maxZoom: 19,
            attribution: 'Tiles © Esri'
        }),
        "Street Map": L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '© OpenStreetMap contributors'
        }),
        "Terrain": L.tileLayer('https://{s}.tile.opentopomap.org/{z}/{x}/{y}.png', {
            maxZoom: 17,
            attribution: 'Map data: © OpenStreetMap contributors, SRTM | Map style: © OpenTopoMap (CC-BY-SA)'
        })
    };

    const overlayLayers = {
        "Street Names": streetOverlay,
        "Terrain Lines": terrainOverlay
    };

    L.control.layers(baseLayers, overlayLayers, {
        position: 'topright',
        collapsed: false
    }).addTo(map);

    // Add scale control
    L.control.scale({
        position: 'bottomleft',
        metric: true,
        imperial: false
    }).addTo(map);

    // Add custom styles for realistic appearance
    const style = document.createElement('style');
    style.textContent = `
        .realistic-tooltip {
            background: rgba(255, 255, 255, 0.95) !important;
            border: 1px solid #ddd !important;
            border-radius: 4px !important;
            color: #333 !important;
            font-weight: 400 !important;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.15) !important;
            font-size: 12px !important;
        }
        .realistic-tooltip::before {
            border-top-color: rgba(255, 255, 255, 0.95) !important;
        }
        .realistic-popup .leaflet-popup-content-wrapper {
            background: white !important;
            border-radius: 8px !important;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15) !important;
        }
        .realistic-popup .leaflet-popup-tip {
            background: white !important;
            border: 1px solid #ddd !important;
        }
        .leaflet-control-layers {
            background: rgba(255, 255, 255, 0.95) !important;
            border-radius: 8px !important;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15) !important;
            border: 1px solid #ddd !important;
        }
        .leaflet-control-layers-title {
            font-weight: 500 !important;
            color: #2c3e50 !important;
            margin-bottom: 8px !important;
        }
        .leaflet-control-scale-line {
            background: rgba(255, 255, 255, 0.8) !important;
            border: 2px solid #333 !important;
            border-radius: 4px !important;
        }
    `;
    document.head.appendChild(style);

    return "Realistic map of Legazpi City loaded successfully";
}