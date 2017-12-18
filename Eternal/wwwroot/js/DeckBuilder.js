var deck;
var cardCount = 0;
var powerCards = 0;
var deckTable = document.getElementById('deck-table');
var row;
var factions = ['Fire', 'Time', 'Justice', 'Primal', 'Shadow'];
var deckFactions = [];

if ($('#deck-list').val() === '')
{
    var deck = [];
} else {

    deck = JSON.parse($('#deck-list').val());
    deck.forEach(function (card) {
        // build table
        row = deckTable.insertRow(-1);
        row.insertCell(0).innerHTML = card.cardName;
        row.insertCell(1).innerHTML = card.count;
        row.insertCell(2).innerHTML = '<button onclick="removeCard(this)" class="btn btn-danger" type="button">X</button>';
        row.id = card.cardId;
        // count cards
        for (var i = 0; i < card.count; i++) {
            cardCount++;
            if (card.type === 'Power') {
                powerCards++;
            }
        }
        $('#cc').text('Deck - ' + cardCount);
    });
    console.log(deck);
}

function addCard(event) {
    var foundCard = false;
    var cardInfo = event.id.split(';');
    var cardId = cardInfo[0];
    var cardName = cardInfo[1];
    var cardFactions = cardInfo[2];
    var cardCost = cardInfo[3];
    var cardType = cardInfo[4];
    if (cardName === 'Fire Sigil' || cardName === 'Time Sigil' || cardName === 'Justice Sigil' || cardName === 'Primal Sigil' || cardName === 'Shadow Sigil') {
        for (var i = 0; i < deck.length; i++) {
            if (deck[i].cardId === cardId && cardCount < 150) {
                deck[i].count++;
                cardCount++;
                powerCards++;
                foundCard = true;
                for (var j = 0; j < deckTable.rows.length; j++) {
                    if (deckTable.rows[j].id === cardId) {
                        deckTable.rows[j].cells[1].innerHTML = deck[i].count;
                        break;
                    }
                }
            }
        }
    } else {
        for (var i = 0; i < deck.length; i++) {
            if (deck[i].cardId === cardId && deck[i].count < 4 && cardCount < 150) {
                deck[i].count++;
                cardCount++;
                if (cardType == 'Power') {
                    powerCards++;
                }
                foundCard = true;
                for (var j = 0; j < deckTable.rows.length; j++) {
                    if (deckTable.rows[j].id === cardId) {
                        deckTable.rows[j].cells[1].innerHTML = deck[i].count;
                        break;
                    }
                }
            } else if (deck[i].cardId === cardId && deck[i].count === 4 && cardCount < 150) {
                foundCard = true;
                break;
            }
        }
    }
    if ((deck.length === 0 || !foundCard) && cardCount < 150) {
        deck.push({ cardId: cardId, count: 1, factions: cardFactions, type: cardType, cardName: cardName, cardCost: cardCost });
        cardCount++;
        if (cardType === 'Power') {
            powerCards++;
        }
        row = deckTable.insertRow(-1);
        row.insertCell(0).innerHTML = cardName;
        row.insertCell(1).innerHTML = 1;
        row.insertCell(2).innerHTML = '<button onclick="removeCard(this)" class="btn btn-danger" type="button">X</button>';
        row.id = cardId;
    }

    if (cardCount >= 75 && cardCount <= 150 && powerCards >= Math.ceil((1 / 3) * cardCount) && powerCards <= Math.floor((2 / 3) * cardCount)) {
        document.getElementById('submit-button').disabled = false;
    } else {
        document.getElementById('submit-button').disabled = true;
    }
    $('#cc').text('Deck - ' + cardCount);
}

function removeCard(event) {
    var rowToChange = event.parentNode.parentNode;
    cardCount--;
    if (rowToChange.childNodes[1].innerHTML === "1") {
        for (var x = 0; x < deck.length; x++) {
            if (rowToChange.id === deck[x].cardId) {
                if (deck[x].type === 'Power') {
                    powerCards--;
                }
                deck.splice(x, 1);
                break;
            }
        }
        rowToChange.parentNode.removeChild(rowToChange);
    } else if (rowToChange.childNodes[1].innerHTML > "1") {
        for (var y = 0; y < deck.length; y++) {
            if (rowToChange.id === deck[y].cardId) {
                deck[y].count--;
                if (deck[y].type === 'Power') {
                    powerCards--;
                }
                break;
            }
        }
        rowToChange.childNodes[1].innerHTML = (parseInt(rowToChange.childNodes[1].innerHTML)) - 1;
    }
    if (cardCount >= 75 && cardCount <= 150 && powerCards >= Math.ceil((1 / 3) * cardCount) && powerCards <= Math.floor((2 / 3) * cardCount)) {
        document.getElementById('submit-button').disabled = false;
    } else {
        document.getElementById('submit-button').disabled = true;
    }
    $('#cc').text('Deck - ' + cardCount);
}

$(document).ready(function () {
    $('#deck-builder-form').submit(function () {
        var factions = ['Fire', 'Time', 'Justice', 'Primal', 'Shadow'];
        deck.forEach(function (card) {
            factions.forEach(function (faction) {
                if (card.factions.includes(faction) && !deckFactions.join(',').includes(faction)) {
                    deckFactions.push(faction);
                }
            });
        });
        console.log(deckFactions);

        if (deckFactions.length > 0) {
            document.forms['create-deck']['Factions'].value = JSON.stringify(deckFactions);
        } else {
            document.forms['create-deck']['Factions'].value = 'Factionless';
        }
        document.forms['create-deck']['DeckList'].value = JSON.stringify(deck);
    });
});