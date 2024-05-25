window.renderCharts = (dataArrays, algorithmNames) => {
    const seriesData = dataArrays.map((dataItems, index) => {
        const datetimeData = dataItems.map(item => {
            const timeParts = item.timeWasted.split(':');
            const hours = parseInt(timeParts[0]);
            const minutes = parseInt(timeParts[1]);
            const seconds = parseInt(timeParts[2]);
            const totalSeconds = hours * 3600 + minutes * 60 + seconds; // переводим время в секунды
            return {
                x: item.elementsCount,
                y: totalSeconds, // используем время в качестве значения оси y
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
        exporting: {
            enabled: true,
            buttons: {
                contextButton: {
                    symbol: 'menu',
                    symbolStroke: '#e0e0e0',
                    theme: {
                        fill: '#2a2a2b',
                        stroke: '#2a2a2b',
                        states: {
                            hover: {
                                fill: '#3e3e40',
                                stroke: '#3e3e40'
                            },
                            select: {
                                fill: '#3e3e40',
                                stroke: '#3e3e40'
                            }
                        }
                    },
                    menuItems: [
                        'printChart',
                        'downloadPNG',
                        'downloadJPEG',
                        'downloadSVG',
                        'downloadCSV',
                        'downloadXLS'
                    ]
                }
            }
        },
        chart: {
            type: 'spline',
            backgroundColor: '#030b12',
            style: {
                fontFamily: '"Arial", sans-serif',
                color: '#e0e0e0'
            }
        },
        title: {
            text: 'Время затраченное на сортировку по количеству элементов',
            style: {
                color: '#ffffff'
            }
        },
        xAxis: {
            type: 'linear',
            title: {
                text: 'Количество элементов',
                style: {
                    color: '#ffffff'
                }
            },
            labels: {
                style: {
                    color: '#e0e0e0'
                }
            },
            lineColor: '#707070',
            tickColor: '#707070'
        },
        yAxis: {
            title: {
                text: 'Время затраченное на сорт. (в сек.)',
                style: {
                    color: '#ffffff'
                }
            },
            labels: {
                format: '{value}',
                style: {
                    color: '#ffffff'
                }
            },
            gridLineColor: '#707070'
        },
        tooltip: {
            formatter: function () {
                return '<span style="color:' + this.point.color + '">\u25CF</span>' + ' <b>' + this.point.algorithmName + '</b><br />' + ' Время: <b>' + Highcharts.dateFormat('%H:%M:%S', this.y * 1000) + '</b><br/>Количество элементов: <b>' + this.x + '</b>';
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
        legend: {
            enabled: true,
            itemStyle: {
                color: '#ffffff'
            },
            itemHoverStyle: {
                color: '#cccccc' // Чуть темнее белого
            }
        },
        navigation: {
            buttonOptions: {
                symbolStroke: '#e0e0e0',
                theme: {
                    fill: '#2a2a2b',
                    stroke: '#2a2a2b',
                    states: {
                        hover: {
                            fill: '#3e3e40',
                            stroke: '#3e3e40'
                        },
                        select: {
                            fill: '#3e3e40',
                            stroke: '#3e3e40'
                        }
                    }
                }
            },
            menuStyle: {
                background: '#2a2a2b',
                color: '#e0e0e0',
                border: '1px solid #3e3e40'
            },
            menuItemStyle: {
                color: '#e0e0e0'
            },
            menuItemHoverStyle: {
                background: '#3e3e40',
                color: '#ffffff'
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
