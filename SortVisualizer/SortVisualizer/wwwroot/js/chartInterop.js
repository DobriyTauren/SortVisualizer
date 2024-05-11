window.renderChart = (dataItems) => {
    const datetimeData = dataItems.map(item => {
        const timeParts = item.timeWasted.split(':');
        const hours = parseInt(timeParts[0]);
        const minutes = parseInt(timeParts[1]);
        const seconds = parseInt(timeParts[2]);
        return {
            x: Date.UTC(1970, 0, 1, hours, minutes, seconds),
            y: item.arrayAccessCount
        };
    });

    datetimeData.sort((a, b) => a.x - b.x);

    Highcharts.chart('chart-container', {
        chart: {
            type: 'area',
            backgroundColor: 'rgba(0, 0, 0, 0.1)', // Прозрачный черный фон (примерно прозрачность 90%)
            style: {
                fontFamily: '"Arial", sans-serif',
                color: '#e0e0e0' // Цвет текста
            }
        },
        title: {
            text: 'Операции во времени',
            style: {
                color: '#ffffff' // Цвет заголовка
            }
        },
        xAxis: {
            type: 'datetime',
            labels: {
                format: '{value:%H:%M:%S}',
                style: {
                    color: '#e0e0e0' // Цвет меток оси X
                }
            },
            lineColor: '#707070', // Цвет линии оси X
            tickColor: '#707070' // Цвет меток на оси X
        },
        yAxis: {
            title: {
                text: 'Количество операций',
                style: {
                    color: '#ffffff' // Цвет заголовка оси Y
                }
            },
            labels: {
                style: {
                    color: '#ffffff' // Цвет меток оси Y
                }
            },
            gridLineColor: '#707070' // Цвет линий сетки
        },
        tooltip: {
            formatter: function () {
                return '<span style="color:' + this.point.color + '">\u25CF</span> Время: <b>' + Highcharts.dateFormat('%H:%M:%S', this.x) + '</b><br/>Количество операций: <b>' + this.y + '</b>';
            },
            backgroundColor: 'rgba(0, 0, 0, 0.8)', // Прозрачный черный фон для тултипа
            borderColor: '#707070', // Цвет рамки тултипа
            style: {
                color: '#e0e0e0' // Цвет текста в тултипе
            }
        },
        series: [{
            name: 'Операции во времени',
            data: datetimeData
        }],
        credits: {
            enabled: false
        },
        exporting: {
            enabled: false
        },
        legend: {
            enabled: false,
        },
        navigation: {
            buttonOptions: {
                enabled: false
            }
        },
        lang: {
            contextButtonTitle: 'Меню',
            decimalPoint: ',',
            downloadCSV: 'Скачать CSV',
            downloadJPEG: 'Скачать JPEG',
            downloadPDF: 'Скачать PDF',
            downloadPNG: 'Скачать PNG',
            downloadSVG: 'Скачать SVG',
            downloadXLS: 'Скачать XLS',
            drillUpText: 'Назад к {series.name}',
            loading: 'Загрузка...',
            months: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
            noData: 'Нет данных для отображения',
            numericSymbols: null,
            printChart: 'Распечатать график',
            resetZoom: 'Сбросить масштаб',
            resetZoomTitle: 'Сбросить уровень масштабирования 1:1',
            shortMonths: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн', 'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'],
            thousandsSep: ' ',
            viewData: 'Просмотр данных в таблице',
            viewFullscreen: 'Во весь экран',
            weekdays: ['Воскресенье', 'Понедельник', 'Вторник', 'Среда', 'Четверг', 'Пятница', 'Суббота']
        }
    });
};
