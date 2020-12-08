define(['knockout', 'postman'], (ko, postman) => {
    
    let currentComponent = ko.observable("home");
    let bodyComponent = ko.observable("home");
    let menuElements = ["Home", "Movie", "Persons", "Create User", "Profile", "About"];
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
        currentComponent,
        bodyComponent,
        menuElements,
        bodyActive,
        changeContent,
        isActive,
        
    };
});
