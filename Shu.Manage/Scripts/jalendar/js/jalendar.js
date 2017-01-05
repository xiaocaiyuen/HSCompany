////////////////////////////////
// Author: Bora DAN — http://codecanyon.net/user/bqra
// 18 August 2013
// E-mail: bora_dan@hotmail.com
////////////////////////////////

$(function () {
    (function ($) {
        $.fn.jalendar = function (options) {
            var settings = $.extend({
                customDay: new Date(),
                color: '#65c2c0',
                lang: 'CN'
            }, options);

            // Languages            
            var dayNames = {};
            var monthNames = {};
            var lAddEvent = {};
            var lAllDay = {};
            var lTotalEvents = {};
            var lEvent = {};
            dayNames['EN'] = new Array('Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun');
            monthNames['EN'] = new Array('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December');
            lAddEvent['EN'] = 'Add New Event';
            lAllDay['EN'] = 'All Day';
            lTotalEvents['EN'] = 'Total Events in This Month: ';
            lEvent['EN'] = 'Event(s)';

            dayNames['CN'] = new Array('一', '二', '三', '四', '五', '六', '日');
            monthNames['CN'] = new Array('1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月');
            lAddEvent['CN'] = '添加新事件';
            lAllDay['CN'] = '整天';
            lTotalEvents['CN'] = '当月所有事件: ';
            lEvent['CN'] = '事件';


            var $this = $(this);
            var div = function (e, classN) {
                return $(document.createElement(e)).addClass(classN);
            };

            // HTML Tree
            $this.append(
                div('div', 'jalendar-wood').append(
                    div('div', 'jalendar-pages').append(
                        div('div', 'pages-bottom'),
                        div('div', 'header').css('background-color', settings.color).append(
                            div('a', 'prv-m'),
                            div('h1'),
                            div('a', 'nxt-m'),
                            div('div', 'day-names')
                        ),
                        div('div', 'total-bar').html(lTotalEvents[settings.lang] + '<b style="color: ' + settings.color + '"></b>'),
                        div('div', 'days clearfix')
                    ),
                    div('div', 'add-event').append(
                          div('div', 'addEvent').html("新增事件"),
                        div('div', 'events').append(
                            div('h3', '').append(
                                div('span', '').html('<b></b> ' + lEvent[settings.lang])
                            ),
                            div('div', 'gradient-wood'),
                            div('div', 'events-list')
                        )
                    )
                )
            );

            // Adding day boxes
            for (var i = 0; i < 42; i++) {
                $this.find('.days').append(div('div', 'day'));
            }

            // Adding day names fields
            for (var i = 0; i < 7; i++) {
                $this.find('.day-names').append(div('h2').text(dayNames[settings.lang][i]));
            }

            var d = new Date(settings.customDay);
            var year = d.getFullYear();
            var date = d.getDate();
            var month = d.getMonth();

            var isLeapYear = function (year1) {
                var f = new Date();
                f.setYear(year1);
                f.setMonth(1);
                f.setDate(29);
                return f.getDate() == 29;
            };

            var feb;
            var febCalc = function (feb) {
                if (isLeapYear(year) === true) { feb = 29; } else { feb = 28; }
                return feb;
            };
            var monthDays = new Array(31, febCalc(feb), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);

            function calcMonth() {

                monthDays[1] = febCalc(feb);

                var weekStart = new Date();
                weekStart.setFullYear(year, month, 0);
                var startDay = weekStart.getDay();

                $this.find('.header h1').html(year + "年" + monthNames[settings.lang][month]);

                $this.find('.event-single').remove();

                $this.find('.day').html('&nbsp;');
                $this.find('.day').removeClass('this-month');
               
                //节假日
                $this.find('.day').removeClass('holidyday');

                var showmonth = month + 1;
                if (showmonth < 10) {
                    showmonth = "0" + showmonth;
                }

                for (var i = 1; i <= monthDays[month]; i++) {
                    var showday = i;
                    if (i < 10) {
                        showday = "0" + i;
                    }

                    startDay++;
                    $this.find('.day').eq(startDay - 1).addClass('this-month').attr('data-date', year + '-' + showmonth + '-' + showday).html(i);
                }
                if (month == d.getMonth()) {
                    $this.find('.day.this-month').removeClass('today').eq(date - 1).addClass('today').css('color', settings.color);
                } else {
                    $this.find('.day.this-month').removeClass('today').attr('style', '');
                }

                // added event
                $this.find('.added-event').each(function (i) {
                    $(this).attr('data-id', i);
                    $(this).attr('data-sid', $(this).attr('data-sid'));
                    $(this).attr('data-eventStatus', $(this).attr('data-eventStatus'));
                    $this.find('.this-month[data-date="' + $(this).attr('data-date') + '"]').append(
                        div('div', 'event-single').attr('data-id', i).append(
                            div('p', '').text($(this).attr('data-title')),
                            div('div', 'details').append(
                                div('div', 'clock').text($(this).attr('data-stime') + " - " + $(this).attr('data-etime')),
                                div('div', 'deleteEvent').html("删除"),
                                div('div', 'editEvent').html("编辑"),
                                div('div', 'status').html($(this).attr('data-eventStatus'))
                            )
                        )
                    );
                    $this.find('.day').has('.event-single').addClass('have-event').prepend(div('i', ''));
                });

                //节假日
                $this.find('.holidy').each(function (i) {
                    $this.find('.this-month[data-date="' + $(this).attr('data-date') + '"]').addClass('holidyday');
                });

                calcTotalDayAgain();
            }

            calcMonth();

            var arrows = new Array($this.find('.prv-m'), $this.find('.nxt-m'));

            // calculate for scroll
            function calcScroll() {
                if ($this.find('.events-list').height() < $this.find('.events').height()) { $this.find('.gradient-wood').hide(); $this.find('.events-list').css('border', 'none') } else { $this.find('.gradient-wood').show(); }
            }

            // Calculate total event again
            function calcTotalDayAgain() {
                var eventCount = $this.find('.this-month .event-single').length;
                $this.find('.total-bar b').text(eventCount);
                $this.find('.events h3 span b').text($this.find('.events .event-single').length)
            }

            function prevAddEvent() {
                $this.find('.day').removeClass('selected').removeAttr('style');
                $this.find('.today').css('color', settings.color);
            }

            //下一个月
            arrows[1].on('click', function () {
                if (month >= 11) {
                    month = 0;
                    year++;
                } else {
                    month++;
                }
                calcMonth();
                prevAddEvent();
            });

            //上个月
            arrows[0].on('click', function () {
                dayClick = $this.find('.this-month');
                if (month === 0) {
                    month = 11;
                    year--;
                } else {
                    month--;
                }
                calcMonth();
                prevAddEvent();
            });

            //月中某天点击事件
            $this.on('click', '.this-month', function () {
                var eventSingle = $(this).find('.event-single')
                $this.find('.events .event-single').remove();
                prevAddEvent();
                $(this).addClass('selected').css({ 'background-color': settings.color });

                $this.children('.jalendar-wood').animate({ width: '1068px' }, 200, function () {
                    $this.find('.add-event').show().find('.events-list').html(eventSingle.clone())
                    calcTotalDayAgain();
                    calcScroll();
                    $this.find('.events h3').css('display', 'block');
                });
            });
        };

    }(jQuery));

});

