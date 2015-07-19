/// <reference path="../jquery-1.7.1-vsdoc.js" />

$(document).ready(function () {	
	poll();
});

function poll() {
	UpdateProjectTilesUsingPartialView();
	setTimeout(poll, 5000);
}

function UpdateProjectTilesUsingPartialView() {	
	$.ajax({		
		url: BuildRequestUrl('ProjectStatusUpdate'),
		type: 'POST',
		success: (function (data) {			
			$('#projectTiles').fadeOut('slow',
				function () { $('#projectTiles').html(data); });
			$('#projectTiles').fadeIn('slow');
		}),
		error: (function () {
			$('#projectTiles').html('<b>An error occurred.</b>');
		})
	});
}

function BuildRequestUrl(action) {
	return 'http://' + location.host + location.pathname + '/' + action;
}