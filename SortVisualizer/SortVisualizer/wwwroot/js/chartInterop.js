window.renderCharts = (dataArrays, algorithmNames) => {
    const seriesData = dataArrays.map((dataItems, index) => {
        const datetimeData = dataItems.map(item => {
            const timeParts = item.timeWasted.split(':');
            const hours = parseInt(timeParts[0]);
            const minutes = parseInt(timeParts[1]);
            const seconds = parseInt(timeParts[2]);
            return {
                x: Date.UTC(1970, 0, 1, hours, minutes, seconds),
                y: item.arrayAccessCount,
                algorithmName: algorithmNames[index] // Добавляем имя алгоритма к данным точек
            };
        });

        datetimeData.sort((a, b) => a.x - b.x);

        return {
            name: algorithmNames[index],
            data: datetimeData
        };
    });

    Highcharts.chart('chart-container', {
        chart: {
            type: 'spline',
            backgroundColor: 'rgba(0, 0, 0, 0.1)',
            style: {
                fontFamily: '"Arial", sans-serif',
                color: '#e0e0e0'
            }
        },
        title: {
            text: 'Операции во времени',
            style: {
                color: '#ffffff'
            }
        },
        xAxis: {
            type: 'datetime',
            labels: {
                format: '{value:%H:%M:%S}',
                style: {
                    color: '#e0e0e0'
                }
            },
            lineColor: '#707070',
            tickColor: '#707070'
        },
        yAxis: {
            title: {
                text: 'Количество операций',
                style: {
                    color: '#ffffff'
                }
            },
            labels: {
                style: {
                    color: '#ffffff'
                }
            },
            gridLineColor: '#707070'
        },
        tooltip: {
            formatter: function () {
                return '<span style="color:' + this.point.color + '">\u25CF</span>' + ' <b>' + this.point.algorithmName + '</b><br />' + ' Время: <b>' + Highcharts.dateFormat('%H:%M:%S', this.x) + '</b><br/>Количество операций: <b>' + this.y + '</b>';
            },
            backgroundColor: 'rgba(0, 0, 0, 0.8)',
            borderColor: '#707070',
            style: {
                color: '#e0e0e0'
            }
        },
        series: seriesData,
        credits: {
            enabled: false
        },
        exporting: {
            enabled: false
        },
        legend: {
            enabled: true,
            itemStyle: {
                color: '#ffffff' // Изменяем цвет текста в легенде
            }
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
