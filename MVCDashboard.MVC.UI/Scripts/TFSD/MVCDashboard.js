/// <reference path="../jquery-1.7.1-vsdoc.js" />


var tileAlertBackground = 'bg-color-red-mod';
var tileWarningBackground = 'bg-color-orange-mod';
var tileOKBackground = 'bg-color-green-mod';
var foregroundColorTransparent = 'fg-color-transparent';

var pollerInterval = 8000;

$(document).ready(function () {
	poll();
});

function UpdateProjectStatus(tile) {
	var tileRef = tile;
	$.ajax({
	    url: "/Testing/GetProjectStatus",
		type: "POST",
		beforeSend: (function () {
			UpdateInProgress(tile);
		}),
		success: (function (data) {
			setTimeout(function () { UpdateTile(data, tile); }, 750);
		}),
		error: (function () {
			UpdateFailed(tile);
		})
	});
}

function UpdateInProgress(tile) {
	$(tile).TileFooter().text('Updating...');
	$(tile).TileName().addClass(foregroundColorTransparent);
}

function UpdateTile(data, tile) {
	$(tile).TileName().text(data.Name).removeClass(foregroundColorTransparent);
	$(tile).TileFooter().text(data.Stage);
	UpdateTileBackgroundColour(data.Status, tile);				
}

function UpdateFailed(tile) {
	$(tile).TileFooter().text('Update retrieval failed.');
	$(tile).TileName().text('');
	UpdateTileBackgroundColour('alert', tile);
}

function UpdateTileBackgroundColour(status, tile) {
	RemoveTileBackgroundCSSClasses(tile);

	var backgroundColour;

	switch (status.toLowerCase()) {
		case 'alert':
			backgroundColour = tileAlertBackground;
			break;
		case 'warning':
			backgroundColour = tileWarningBackground;
			break;
		case 'ok':
			backgroundColour = tileOKBackground;
			break;
		default:
			backgroundColour = '';
	}

	$(tile).addClass(backgroundColour);
}

function RemoveTileBackgroundCSSClasses(tile) {
	$(tile)
		.removeClass(tileAlertBackground)
		.removeClass(tileWarningBackground)
		.removeClass(tileOKBackground);
}

function poll() {
	poller = setTimeout(poll, pollerInterval);
	setTimeout(function () { UpdateProjectStatus('#projectTile0') }, 0);
	setTimeout(function () { UpdateProjectStatus('#projectTile1') }, 1000);
	setTimeout(function () { UpdateProjectStatus('#projectTile2') }, 2000);
	setTimeout(function () { UpdateProjectStatus('#projectTile3') }, 3000);
	setTimeout(function () { UpdateProjectStatus('#projectTile4') }, 4000);
	setTimeout(function () { UpdateProjectStatus('#projectTile5') }, 5000);
}

jQuery.fn.extend(
{
	TileName: function () {
		return $(this).find('.tileName');
	},
	TileFooter: function () {
		return $(this).find('.tileFooter');
	}
})