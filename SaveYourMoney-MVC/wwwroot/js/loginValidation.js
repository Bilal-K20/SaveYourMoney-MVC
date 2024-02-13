
/**

Validation for the login page 

**/

document.getElementById('loginForm').addEventListener('submit', function (event) {
        // Prevent default form submission
        event.preventDefault();

    // Collect form data
    var formData = new FormData(document.getElementById('loginForm'));

    // Client-side validation
    var username = formData.get('Username');
    var password = formData.get('Password');

    var usernameRegex = /^[^\s@@]+$/; // Regular expression to match any non-empty string that is not an email address
    if (!username || username.length < 3 || !usernameRegex.test(username)) {
        alert('Username must be at least three characters long and cannot be an email address.');
    return; // Stop form submission if validation fails
        }
    if (!password) {
        alert('Please enter a password.');
    return; // Stop form submission if validation fails
        }

    // AJAX request to submit form data to server
    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/LoginAndSignUp/Login'); // Replace with your actual controller action endpoint
    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded'); // Set content type
    xhr.onload = function () {
            if (xhr.status === 200) {
                // Handle successful response from server
                var response = JSON.parse(xhr.responseText);
    if (response.success) {
        // If login was successful, redirect to dashboard
        window.location.href = response.redirectTo;
                } else {
        // If login was not successful, display error message
        alert("Login failed: " + response.message);
                }
            } else {
        // Handle error response from server
        console.error('Error:', xhr.statusText);
    alert("Login failed. Please try again later.");
            }
        };
    xhr.onerror = function () {
        // Handle network errors
        console.error('Request failed due to network errors.');
    alert("Login failed. Please try again later.");
        };
    // Send form data as URL-encoded string
    xhr.send(new URLSearchParams(formData).toString());
});

