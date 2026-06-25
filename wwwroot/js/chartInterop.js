window.renderCharts = (dataArrays, algorithmNames) => {
    const seriesData = dataArrays.map((dataItems, index) => {
        const datetimeData = dataItems.map(item => {
            return {
                x: item.elementsCount,
                y: typeof item.sortTimeMs === 'number' ? item.sortTimeMs : 0, // реальное время вычисления (мс)
                algorithmName: algorithmNames[index]
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
        colors: ['#22D3EE', '#818CF8', '#A78BFA', '#34D399', '#F471B5', '#FBBF24', '#60A5FA', '#FB7185'],
        chart: {
            type: 'spline',
            backgroundColor: 'transparent',
            style: {
                fontFamily: '"Inter", "Roboto", sans-serif',
                color: '#9DA9C2'
            }
        },
        title: {
            text: 'Реальное время вычисления по количеству элементов',
            style: {
                color: '#F4F8FF',
                fontFamily: '"Space Grotesk", "Inter", sans-serif',
                fontWeight: '600'
            }
        },
        xAxis: {
            type: 'linear',
            title: {
                text: 'Количество элементов',
                style: {
                    color: '#C7D2E5'
                }
            },
            labels: {
                style: {
                    color: '#9DA9C2'
                }
            },
            lineColor: 'rgba(255, 255, 255, 0.15)',
            tickColor: 'rgba(255, 255, 255, 0.15)'
        },
        yAxis: {
            title: {
                text: 'Время вычисления (мс)',
                style: {
                    color: '#C7D2E5'
                }
            },
            labels: {
                format: '{value}',
                style: {
                    color: '#9DA9C2'
                }
            },
            gridLineColor: 'rgba(255, 255, 255, 0.07)'
        },
        plotOptions: {
            spline: {
                lineWidth: 3,
                marker: { radius: 4, symbol: 'circle', lineWidth: 0 },
                states: { hover: { lineWidth: 4 } }
            }
        },
        tooltip: {
            formatter: function () {
                return '<span style="color:' + this.point.color + '">\u25CF</span>' + ' <b>' + this.point.algorithmName + '</b><br />' + ' Время: <b>' + Highcharts.numberFormat(this.y, 3) + ' мс</b><br/>Количество элементов: <b>' + this.x + '</b>';
            },
            backgroundColor: 'rgba(17, 21, 31, 0.92)',
            borderColor: 'rgba(99, 102, 241, 0.45)',
            borderRadius: 12,
            style: {
                color: '#E6EDF8'
            }
        },
        series: seriesData,
        credits: {
            enabled: false
        },
        legend: {
            enabled: true,
            itemStyle: {
                color: '#C7D2E5'
            },
            itemHoverStyle: {
                color: '#ffffff'
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
