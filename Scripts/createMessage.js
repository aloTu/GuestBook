let submit = document.querySelector("input[type='submit']");
let toast = document.querySelector(".toast");
let processBar = document.querySelector(".progress-bar");
let closeBtn = document.querySelector(".ml-2");
let errormsg = document.querySelector(".toast-body").firstElementChild;

window.onload = () => {
    console.log(errormsg.className);
    if (errormsg.className == "validation-summary-errors") {
        toast.classList.add("showtoast");
        processBar.classList.add("waited");
        setTimeout(closeToast, 5000);
    }
    submit.addEventListener('click', () => {
        toast.classList.add("showtoast");
        processBar.classList.add("waited");
        setTimeout(closeToast, 5000);
    })
    closeBtn.addEventListener('click', closeToast);
}
function closeToast() {
    toast.classList.remove("showtoast");
    processBar.classList.remove("waited");
}