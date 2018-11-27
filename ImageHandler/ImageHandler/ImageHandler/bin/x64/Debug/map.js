var items = JSON.parse(picturesFile);
class Picture //Класс картины
{
    constructor(item) {
        this.name = item['Name'];
        this.author = item['Author'];
        this.yearOfCreation = item['YearOfCreation'];

        this.percentOfRed = item['PercentOfRed'];
        this.percentOfGreen = item['PercentOfGreen'];
        this.percentOfBlue = item['PercentOfBlue'];
        this.height = item['Height'];
        this.width = item['Width'];

        this.longitude = item['Longitude'];
        this.latitude = item['Latitude'];
        this.longitudeCreation = item['LongitudeCreation'];
        this.latitudeCreation = item['LatitudeCreation'];
        this.longitudeStorage = item['LongitudeStorage'];
        this.latitudeStorage = item['LatitudeStorage'];
      }
}

var mas = [];
for (var i = 0; i < items.length; i++){
    mas.push(new Picture(items[i]));
}

DG.then(function() {
    var map, coords = [], rectangles = [];  //Основные переменные. map - объект карты, rectangles - массив выделенных областей
    map = DG.map('map', { //Инициализация карты
        center: [54.98, 82.89],
        zoom: 3,
        doubleClickZoom: false, //Отключение увеличения карты при двойном , тк это событие используется для выделение области
        poi: false //Отключение отображения точек интереса на карте
    });

    for (var i = 0; i < mas.length; i++){ //Создание и отображение меток, отображающих картины
        var markerr = DG.marker([mas[i].longitude, mas[i].latitude]);

        var myPopUp = DG.popup({ //Создание попапа - описания метки
            maxWidth: 350,
            minWidth: 230
            })
            .setContent('<p>Название: ' + mas[i].name
                        + ' <br />Автор: ' + mas[i].author
                        + ' <br />Год создания: ' + mas[i].yearOfCreation
                        + ' <br />Высота: ' + mas[i].height
                        + ' <br />Ширина: ' + mas[i].width
                        + ' <br />Содержание цветов:'
                        + ' <br />Красного: ' + mas[i].percentOfRed + '%'
                        + ' <br />Зелёного: ' + mas[i].percentOfGreen + '%'
                        + ' <br />Синего: ' + mas[i].percentOfBlue + '% </p>');
        markerr.bindPopup(myPopUp); //Прикрепление созданного попапа к метке

        markerr.on('click', function(e) { //Центрирование карты на метке, при клике на неё
            map.setView([e.latlng.lat, e.latlng.lng]);
        });
        markerr.addTo(map); //Добавление метки на карту
    }

    map.on('dblclick', function(e) { //Выделение области, создание прямоугольников
            coords.push(e.latlng); //Добавляем в масмив координаты клика
            if (coords.length === 2){ //При первом двойном клике создается прямоугольник
                var rectangle = DG.rectangle(coords);

                rectangle.on('contextmenu', function(e) { //При нажатии на правую кнопку прямоугольник удаляется из массива и карты
                    rectangles.splice(rectangles.indexOf(e.target), 1);
                    e.target.remove();
                });

                rectangle.on('click', function(e) { //При нажатии на левую кнопку отображаем статистику выбранной области
                    var area = DG.latLngBounds(e.target.getLatLngs()); //Получаем границы области, на которую щелкнули

                    var count = 0, averageHeight = 0, averageWidth = 0, averagePercentOfRed = 0, averagePercentOfGreen = 0, averagePercentOfBlue = 0;
                    for (var i = 0; i < mas.length; i++) { //Перебираем массив точек и проверяем, какие из них попали в выделенную область
                        if (area.contains([mas[i].longitude, mas[i].latitude])){
                            count++; //Считаем колчиество картин в области
                            averageHeight += mas[i].height; //Высчитываем средни значения
                            averageWidth += mas[i].width;
                            averagePercentOfRed += mas[i].percentOfRed;
                            averagePercentOfGreen += mas[i].percentOfGreen;
                            averagePercentOfBlue += mas[i].percentOfBlue;
                        }
                    }
                    averageHeight /= count;
                    averageWidth /= count;
                    averagePercentOfRed /= count;
                    averagePercentOfGreen /= count;
                    averagePercentOfBlue /= count;

                    var myPopUp = DG.popup({ //Создание попапа - описания области
                        maxWidth: 500,
                        minWidth: 270
                    }).setContent('<p>Количество картин в области: ' + count
                            + ' <br />Средние значения: '
                            + ' <br />  Высота картины: ' + averageHeight.toFixed(2)
                            + ' <br />  Ширина картины: ' + averageWidth.toFixed(2)
                            + ' <br />  Процент красного: ' + averagePercentOfRed.toFixed(2) + '%'
                            + ' <br />  Процент зеленого: ' + averagePercentOfGreen.toFixed(2) + '%'
                            + ' <br />  Процент синего: ' + averagePercentOfBlue.toFixed(2) + '% </p>');
                    e.target.bindPopup(myPopUp).openPopup(); //Прикрепление созданного попапа к области
                });

                rectangles.push(rectangle); //Добавляем прямоугольник в массив областей
                rectangles[rectangles.length - 1].addTo(map); //Отображаем прямоугольник на карте
            }
            if(coords.length === 4){ //При втором двойном щелчке растягиваем созданным ранее прямоугольник до нужного размера
                rectangles[rectangles.length - 1].setBounds([coords[0], coords[2]]);
                coords = [];
            }
    });
});




