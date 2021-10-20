// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.toast').toast(option);
function toggleShowPassword() {
	var passwordTextBox = document.getElementById('passwordTextBox');
	if (passwordTextBox.getAttribute('type') == 'text') {
		passwordTextBox.type = 'password';
	}
	else {
		passwordTextBox.type = 'text';
	}
}