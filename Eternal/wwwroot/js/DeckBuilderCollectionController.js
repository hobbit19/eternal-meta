var factions = [];
var factionFilter = document.forms['card-filters']['factionFilter'];
var costFilter = document.forms['card-filters']['costFilter'];

/*if (factionFilter.value !== '') {
    factions = JSON.parse(factionFilter.value);
    for (var i = 0; i < factions.length; i++) {
        $('#' + factions[i]).attr('src', '/images/icons/factions/' + factions[i].toLowerCase() + '-icon-select.png');
        $('#' + factions[i]).attr('id', factions[i] + '-Select');
    }
}

if (costFilter.value !== '') {
    $('#' + costFilter.value)
        .attr('src', '/images/icons/power/' + costFilter.value + '-select.png')
        .attr('id', costFilter.value + '-select');
}*/

$('.faction-icon')
    .mouseenter(function () {
        switch (this.id) {
            case 'Fire':
                $(this).attr('src', '/images/icons/factions/fire-icon-select.png');
                break;
            case 'Time':
                $(this).attr('src', '/images/icons/factions/time-icon-select.png');
                break;
            case 'Justice':
                $(this).attr('src', '/images/icons/factions/justice-icon-select.png');
                break;
            case 'Primal':
                $(this).attr('src', '/images/icons/factions/primal-icon-select.png');
                break;
            case 'Shadow':
                $(this).attr('src', '/images/icons/factions/shadow-icon-select.png');
                break;
            case 'Multifaction':
                $(this).attr('src', '/images/icons/factions/multifaction-icon-select.png');
                break;
            case 'Factionless':
                $(this).attr('src', '/images/icons/factions/factionless-icon-select.png');
                break;
        }
    })
    .mouseleave(function () {
        switch (this.id) {
            case 'Fire':
                $(this).attr('src', '/images/icons/factions/fire-icon.png');
                break;
            case 'Time':
                $(this).attr('src', '/images/icons/factions/time-icon.png');
                break;
            case 'Justice':
                $(this).attr('src', '/images/icons/factions/justice-icon.png');
                break;
            case 'Primal':
                $(this).attr('src', '/images/icons/factions/primal-icon.png');
                break;
            case 'Shadow':
                $(this).attr('src', '/images/icons/factions/shadow-icon.png');
                break;
            case 'Multifaction':
                $(this).attr('src', '/images/icons/factions/multifaction-icon.png');
                break;
            case 'Factionless':
                $(this).attr('src', '/images/icons/factions/factionless-icon.png');
                break;
        }
    })
    .click(function () {
        switch (this.id) {
            case 'Fire':
                factions.push(this.id);
                factionFilter.value = JSON.stringify(factions);
                $(this).attr('src', '/images/icons/factions/fire-icon-select.png');
                $(this).attr('id', 'Fire-Select');
                break;
            case 'Fire-Select':
                $(this).attr('src', '/images/icons/factions/fire-icon.png');
                $(this).attr('id', 'Fire');
                var deselectedFaction = this.id;
                factions = factions.filter(function (faction) {
                    return faction !== deselectedFaction;
                });
                if (factions.length === 0) {
                    factionFilter.value = '';
                } else {
                    factionFilter.value = JSON.stringify(factions);
                }
                break;
            case 'Time':
                factions.push(this.id);
                factionFilter.value = JSON.stringify(factions);
                $(this).attr('src', '/images/icons/factions/time-icon-select.png');
                $(this).attr('id', 'Time-Select');
                break;
            case 'Time-Select':
                $(this).attr('src', '/images/icons/factions/time-icon.png');
                $(this).attr('id', 'Time');
                var deselectedFaction = this.id;
                factions = factions.filter(function (faction) {
                    return faction !== deselectedFaction;
                });
                if (factions.length === 0) {
                    factionFilter.value = '';
                } else {
                    factionFilter.value = JSON.stringify(factions);
                }
                break;
            case 'Justice':
                factions.push(this.id);
                factionFilter.value = JSON.stringify(factions);
                $(this).attr('src', '/images/icons/factions/justice-icon-select.png');
                $(this).attr('id', 'Justice-Select');
                break;
            case 'Justice-Select':
                $(this).attr('src', '/images/icons/factions/justice-icon.png');
                $(this).attr('id', 'Justice');
                var deselectedFaction = this.id;
                factions = factions.filter(function (faction) {
                    return faction !== deselectedFaction;
                });
                if (factions.length === 0) {
                    factionFilter.value = '';
                } else {
                    factionFilter.value = JSON.stringify(factions);
                }
                break;
            case 'Primal':
                factions.push(this.id);
                factionFilter.value = JSON.stringify(factions);
                $(this).attr('src', '/images/icons/factions/primal-icon-select.png');
                $(this).attr('id', 'Primal-Select');
                break;
            case 'Primal-Select':
                $(this).attr('src', '/images/icons/factions/primal-icon.png');
                $(this).attr('id', 'Primal');
                var deselectedFaction = this.id;
                factions = factions.filter(function (faction) {
                    return faction !== deselectedFaction;
                });
                if (factions.length === 0) {
                    factionFilter.value = '';
                } else {
                    factionFilter.value = JSON.stringify(factions);
                }
                break;
            case 'Shadow':
                factions.push(this.id);
                factionFilter.value = JSON.stringify(factions);
                $(this).attr('src', '/images/icons/factions/shadow-icon-select.png');
                $(this).attr('id', 'Shadow-Select');
                break;
            case 'Shadow-Select':
                $(this).attr('src', '/images/icons/factions/shadow-icon.png');
                $(this).attr('id', 'Shadow');
                var deselectedFaction = this.id;
                factions = factions.filter(function (faction) {
                    return faction !== deselectedFaction;
                });
                if (factions.length === 0) {
                    factionFilter.value = '';
                } else {
                    factionFilter.value = JSON.stringify(factions);
                }
                break;
            case 'Multifaction':
                factions.push(this.id);
                factionFilter.value = JSON.stringify(factions);
                $(this).attr('src', '/images/icons/factions/multifaction-icon-select.png');
                $(this).attr('id', 'Multifaction-Select');
                break;
            case 'Multifaction-Select':
                $(this).attr('src', '/images/icons/factions/multifaction-icon.png');
                $(this).attr('id', 'Multifaction');
                var deselectedFaction = this.id;
                factions = factions.filter(function (faction) {
                    return faction !== deselectedFaction;
                });
                if (factions.length === 0) {
                    factionFilter.value = '';
                } else {
                    factionFilter.value = JSON.stringify(factions);
                }
                break;
            case 'Factionless':
                factions.push(this.id);
                factionFilter.value = JSON.stringify(factions);
                $(this).attr('src', '/images/icons/factions/factionless-icon-select.png');
                $(this).attr('id', 'Factionless-Select');
                break;
            case 'Factionless-Select':
                $(this).attr('src', '/images/icons/factions/factionless-icon.png');
                $(this).attr('id', 'Factionless');
                var deselectedFaction = this.id;
                factions = factions.filter(function (faction) {
                    return faction !== deselectedFaction;
                });
                if (factions.length === 0) {
                    factionFilter.value = '';
                } else {
                    factionFilter.value = JSON.stringify(factions);
                }
                break;
        }
    });

$('.power-icon')
    .mouseenter(function () {
        switch (this.id) {
            case '0':
                $(this).attr('src', '/images/icons/power/0-select.png');
                break;
            case '1':
                $(this).attr('src', '/images/icons/power/1-select.png');
                break;
            case '2':
                $(this).attr('src', '/images/icons/power/2-select.png');
                break;
            case '3':
                $(this).attr('src', '/images/icons/power/3-select.png');
                break;
            case '4':
                $(this).attr('src', '/images/icons/power/4-select.png');
                break;
            case '5':
                $(this).attr('src', '/images/icons/power/5-select.png');
                break;
            case '6':
                $(this).attr('src', '/images/icons/power/6-select.png');
                break;
            case '7':
                $(this).attr('src', '/images/icons/power/7-select.png');
                break;
        }
    })
    .mouseleave(function () {
        switch (this.id) {
            case '0':
                $(this).attr('src', '/images/icons/power/0.png');
                break;
            case '1':
                $(this).attr('src', '/images/icons/power/1.png');
                break;
            case '2':
                $(this).attr('src', '/images/icons/power/2.png');
                break;
            case '3':
                $(this).attr('src', '/images/icons/power/3.png');
                break;
            case '4':
                $(this).attr('src', '/images/icons/power/4.png');
                break;
            case '5':
                $(this).attr('src', '/images/icons/power/5.png');
                break;
            case '6':
                $(this).attr('src', '/images/icons/power/6.png');
                break;
            case '7':
                $(this).attr('src', '/images/icons/power/7.png');
                break;
        }
    })
    .click(function () {

        for (var i = 0; i <= 7; i++) {
            if ((i.toString() + '-select') !== this.id) {
                $('#' + i + '-select')
                    .attr('src', '/images/icons/power/' + i + '.png')
                    .attr('id', i.toString());
            }
        }

        switch (this.id) {
            case '0':
                costFilter.value = 0;
                $(this).attr('src', '/images/icons/power/0-select.png');
                $(this).attr('id', '0-select');
                break;
            case '0-select':
                costFilter.value = '';
                $(this).attr('src', '/images/icons/power/0.png');
                $(this).attr('id', '0');
                break;
            case '1':
                costFilter.value = 1;
                $(this).attr('src', '/images/icons/power/1-select.png');
                $(this).attr('id', '1-select');
                break;
            case '1-select':
                costFilter.value = '';
                $(this).attr('src', '/images/icons/power/1.png');
                $(this).attr('id', '1');
                break;
            case '2':
                costFilter.value = 2;
                $(this).attr('src', '/images/icons/power/2-select.png');
                $(this).attr('id', '2-select');
                break;
            case '2-select':
                costFilter.value = '';
                $(this).attr('src', '/images/icons/power/2.png');
                $(this).attr('id', '2');
                break;
            case '3':
                costFilter.value = 3;
                $(this).attr('src', '/images/icons/power/3-select.png');
                $(this).attr('id', '3-select');
                break;
            case '3-select':
                costFilter.value = '';
                $(this).attr('src', '/images/icons/power/3.png');
                $(this).attr('id', '3');
                break;
            case '4':
                costFilter.value = 4;
                $(this).attr('src', '/images/icons/power/4-select.png');
                $(this).attr('id', '4-select');
                break;
            case '4-select':
                costFilter.value = '';
                $(this).attr('src', '/images/icons/power/4.png');
                $(this).attr('id', '4');
                break;
            case '5':
                costFilter.value = 5;
                $(this).attr('src', '/images/icons/power/5-select.png');
                $(this).attr('id', '5-select');
                break;
            case '5-select':
                costFilter.value = '';
                $(this).attr('src', '/images/icons/power/5.png');
                $(this).attr('id', '5');
                break;
            case '6':
                costFilter.value = 6;
                $(this).attr('src', '/images/icons/power/6-select.png');
                $(this).attr('id', '6-select');
                break;
            case '6-select':
                costFilter.value = '';
                $(this).attr('src', '/images/icons/power/6.png');
                $(this).attr('id', '6');
                break;
            case '7':
                costFilter.value = 7;
                $(this).attr('src', '/images/icons/power/7-select.png');
                $(this).attr('id', '7-select');
                break;
            case '7-select':
                costFilter.value = '';
                $(this).attr('src', '/images/icons/power/7.png');
                $(this).attr('id', '7');
                break;
        }
    });

$('#reset-filter-link').click(function (e) {
    e.preventDefault();
    document.forms['card-filters']['searchFilter'].value = '';
    document.forms['card-filters']['factionFilter'].value = '';
    var cost = document.forms['card-filters']['costFilter'].value;
    document.forms['card-filters']['costFilter'].value = '';
    for (var i = 0; i < factions.length; i++) {
        $('#' + factions[i] + '-Select')
            .attr('src', '/images/icons/factions/' + factions[i].toLowerCase() + '-icon.png')
            .attr('id', factions[i]);
    }
    $('#' + cost + '-select')
        .attr('src', '/images/icons/power/' + cost + '.png')
        .attr('id', cost);
    factions = [];
});



$('#card-filters').on('submit', function (e) {
    e.preventDefault();
    var searchFilter = document.forms['card-filters']['searchFilter'].value;
    console.log(searchFilter);
    /*var searchString = $('#searchString').val();
    var factionFilter = $('#factionFilter').val();
    var costFilter = $('#costFilter').val();
    // probably change code directly below.
    var factions = '';
    factionFilter.each(function () {
        factions += $(this).val();
    });*/

    $.get('/Decks/LoadCollection', { searchFilter: searchFilter, factionFilter: factionFilter.value, costFilter: costFilter.value }, function (data) {
        $('.collection-vm').replaceWith(data);
    });
});

function changePage(page, currentSearchFilter, currentFactionFilter, currentCostFilter) {
    $.get('/Decks/LoadCollection', { page: page, currentSearchFilter: currentSearchFilter, currentFactionFilter: currentFactionFilter, currentCostFilter: currentCostFilter }, function (data) {
        $('.collection-vm').replaceWith(data);
    });
}

