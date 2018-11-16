ymaps.ready(init);
var myMap;
function init(){
    myMap = new ymaps.Map("map", {
        center: [55.994416, 92.797207],
        zoom: 15,
        controls: ['zoomControl']
    });

}

var myCoords;

function httpGet(theUrl)
{
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open( "GET", theUrl, false ); // false for synchronous request
    xmlHttp.send( null );
    return xmlHttp.responseText;
}

function findHome() {
    var myGeocoder = ymaps.geocode(document.getElementById('addressBox').value, {kind: 'house'});
    myGeocoder.then(
        function (res) {
            myCoords = res.geoObjects.get(0).geometry.getCoordinates();
            myMap.setCenter(myCoords, 13);
            document.getElementById("location").innerHTML = myCoords;
            myMap.geoObjects.add(
                new ymaps.Placemark(myCoords,
                    {iconContent: 'Наш дом?'},
                    {preset: 'islands#greenStretchyIcon'}
                )
            );
            var myCircle = new ymaps.Circle([myCoords, 500]);
            myMap.geoObjects.add(myCircle);


            var item = document.getElementById('itemBox').value;
            var jso = httpGet('https://search-maps.yandex.ru/v1/?text='+item+'&ll=' + myCoords[1] + ',' + myCoords[0] + '&spn=1,1&rspn=1&results=500&lang=ru_RU&apikey=bd40169f-0453-4ffb-a272-373d2712ec55');
            var objects = JSON.parse(jso);

            document.getElementById('eBox').value = jso;

            for (var i = 0; i < objects.features.length; i++){
                addrows(objects.features[i].properties.CompanyMetaData.name, objects.features[i].properties.CompanyMetaData.address, objects.features[i].properties.CompanyMetaData.Categories[0].name);
                var house = ymaps.geocode('Красноярск, ' + objects.features[i].properties.CompanyMetaData.address, {kind: 'house'});
                house.then(
                    function (ress) {
                        myMap.geoObjects.add(
                            new ymaps.Placemark(ress.geoObjects.get(0).geometry.getCoordinates(),
                                {iconContent: item+'?'},
                                {preset: 'islands#greenStretchyIcon'}
                            )
                        );
                    },
                    function (err) {
                        alert('Ошибка2');
                    }
                );
            }
        },
        function (err) {
            alert('Ошибка');
        }
    );

}

function addrows(name, address, type) {
    var newRow = document.createElement('tr');
    var table = document.getElementsByClassName("tbodyy")[0];

    var newColumn = document.createElement('td'); newColumn.innerHTML = name;
    var newColumn1 = document.createElement('td'); newColumn1.innerHTML = address;
    var newColumn2 = document.createElement('td'); newColumn1.innerHTML = type;
    newRow.appendChild(newColumn);
    newRow.appendChild(newColumn1);
    newRow.appendChild(newColumn2);
    newRow.className = "roww";
    table.appendChild(newRow);

}

