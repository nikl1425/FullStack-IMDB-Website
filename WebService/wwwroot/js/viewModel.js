define(['knockout', 'postman'], (ko, postman) => {

    let currentComponent = ko.observable("home");
    let bodyComponent = ko.observable("home");
    let menuElements = ["Home", "Movie", "People", "Profile"];
    let subElements = ["Peoplepage"];
    let movieElement = ["Moviepage"];
    let listcreateElement = ["Createlist"];
    let listElement = ["Listpage"];
    let loginElement = ["Login"];
    let registerElement = ["Register"];
    let updateContentElement = ["Updateuser"];
    
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

    function goToHome(){
        postman.publish("changeContent", "home");
    }

    $(document).on('click', '.navbar-brand', function() {
        goToHome()
    });
    
    $(document).on('click', '.footer-logo', function() {
        goToHome()
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
        loginElement,
        registerElement,
        updateContentElement

    };
});