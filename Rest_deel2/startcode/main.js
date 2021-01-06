/* 
 * Deze javascript code test het opvragen van workshops in de Web API van het labo over ASP MVC
 */

let main = () => {
    let url = "https://localhost:44333/api/NewsMessages";

    
    
    $.getJSON(url).then((result) => {
        console.log(result);

        for (let newsMessage of result) {
            // https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Template_literals
            let content = `
                <div class="card mb-4">
                    <div class="card-header">${newsMessage.title}</div>
                    <div class="card-body">
                        <p class="card-text">Message: ${newsMessage.message}. Opgesteld op ${newsMessage.date}</p>
                    </div>
                </div>`;
            $("main").append(content);
        }
    });
};

$(main);