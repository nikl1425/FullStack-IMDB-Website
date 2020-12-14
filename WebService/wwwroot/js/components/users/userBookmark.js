define(['knockout', 'postman'], (ko, postman) => {
    return function () {
        console.log(window.listValue);
        //window.movieValue = "";
                
        let tListData = ko.observableArray([])
        let tBookmarks = ko.observableArray([])
        
        let pListData = ko.observableArray([])
        let pBookmarks = ko.observableArray([])
        
        const tlistUrl = 'http://localhost:5001/api/tlist/';
        const plistUrl = 'http://localhost:5001/api/plist/';

        function goToMoviePage(){
            postman.publish("changeContent", "moviePage");
        }
        
        if(window.listValue.indexOf('t')>-1) {
            let listId = window.listValue.substring(1);
            console.log("listid: "+listId);
            console.log(tlistUrl+listId);
            fetch(tlistUrl + listId)
                .then((response) => {
                    return response.json()
                })
                .then((data) => {
                    tListData(data.titleList)
                    tBookmarks(data.tbookmarkDtos)
                    console.log("TLISTDATA: "+data.titleList)
                    console.log("TBOOKMARKS: "+data.tbookmarkDtos)
                }).catch((err) => {
            })
        }
        if(window.listValue.indexOf('p')>-1) {
            let listId = window.listValue.substring(1);
            console.log("listid: "+listId);
            console.log(plistUrl+listId);
            fetch(plistUrl + listId)
                .then((response) => {
                    return response.json()
                })
                .then((data) => {
                    pListData(data.plistDto)
                    pBookmarks(data.pbookmarkDtos)
                    $(".gotomoviepage").on('click', function(){
                        window.movieValue = $(this).val();
                        console.log($(this).val());
                        goToMoviePage();
                    })
                }).catch((err) => {
            })
        }

        function gotoPeoplePage(){
            postman.publish("changeContent", "peoplePage");
        }
        
        $(document).on('click', '.goToPeoplePage', function() {
            window.value = $(this).val();
            console.log(window.value)
            gotoPeoplePage()
        });
        

        return {
            pListData,
            pBookmarks,
            tListData,
            tBookmarks,
            postman
        };
    }
});