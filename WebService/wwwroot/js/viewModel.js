define(['knockout', 'postman'], (ko, postman) => {

    let currentComponent = ko.observable("home");
    let bodyComponent = ko.observable("home");
    let menuElements = ["Home", "Movie", "People", "Profile", "Login", "Register", "About"];
    let subElements = ["Peoplepage"];
    let movieElement = ["Moviepage"];
    let listcreateElement = ["Createlist"];
    let listElement = ["Listpage"];
    let loginElement = ["Loginpage"];
    
    let changeContent = element => {
        currentComponent(element.toLowerCase());
        bodyComponent = currentComponent;
    }

    let bodyActive = element => {
        bodyComponent(element.toLowerCase());
    }

    let isActive = element => {
        return element.toLowerCase() === currentComponent() ? "active" : "";
    }




    postman.subscribe("changeContent", component => {
        changeContent(component);
    });


    
    return {
        subElements,
        currentComponent,
        bodyComponent,
        menuElements,
        bodyActive,
        changeContent,
        isActive,
        movieElement,
        listcreateElement,
        listElement,
        loginElement

    };
});