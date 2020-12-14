define(['knockout', 'postman', 'dataservice'], (ko, postman, dataService) => {
        return function () {
            let peopleList = ko.observableArray([]);
            let pageSizes = ko.observableArray();
            let prev = ko.observableArray();
            let next = ko.observableArray();
            let selectedPage = ko.observableArray([10]);

            /* OLD FETCH
                    fetch('http://localhost:5001/api/name')
                        .then(function (response) {
                            return response.json();
                        })
                        .then(function (data) {
                            people(data);
                            console.log(people())
                        })
            */
            function goToPersonPage() {
                postman.publish("changeContent", "peoplePage");
            }

            $(document).on('click', '.fromPersonListToPage', function () {
                window.value = $(this).val();
                console.log(window.value)
                goToPersonPage()
            });

            document.getElementById("scrolltotop").addEventListener("click", function () {
                console.log("Clicked!");
                $('html,body').animate({scrollTop: $('#scrolltothisdiv').offset().top}, 1000);
            });

            document.getElementById("prevscrolltotop").addEventListener("click", function () {
                console.log("Clicked!");
                $('html,body').animate({scrollTop: $('#scrolltothisdiv').offset().top}, 1000);
            });

            //PAGING


            let getPersons = url => {
                dataService.getPersons(url, data => {
                        pageSizes(data.pageSizes);
                        prev(data.prev || undefined);
                        next(data.next || undefined);
                        peopleList(data.personList);
                        console.log(peopleList());
                        $(document).on('click', '.fromPersonListToPage', function () {
                            window.value = $(this).val();
                            console.log(window.value)
                            goToPersonPage()
                        });


                    }
                )
            };


            let showPrev = movie => {
                console.log(prev());
                getPersons(prev());
            }

            let enablePrev = ko.computed(() => prev() !== undefined);

            let showNext = product => {
                console.log(next());
                getPersons(next());
            }

            let enableNext = ko.computed(() => next() !== undefined);

            selectedPage.subscribe(() => {
                var size = selectedPage()[0];
                getPersons(dataservice.getMoviesUrlWithPageSize(size));
            });

// PAGING END

            getPersons();


            return {
                peopleList,
                postman,
                showPrev,
                enableNext,
                enablePrev,
                showNext,
                pageSizes


            };

        }
    }
)
;