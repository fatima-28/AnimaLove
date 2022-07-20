 let maindiv = document.querySelector('.container');
        fetch('https://api.thecatapi.com/v1/images/search')
            .then(res => res.json())
            .then(data => data.forEach(item => {
                let div = `<div class="column filterDiv cat">
                              
                                <div class="card-item ">
                                    <div class="animal-image">
                                                          <img src="${item.url}" alt="animal image">
                                    </div>
                                    <div class="animal-context">
                                        <p class="animal-name">Barrot</p>
                                        <p class="animal-message">Hello my name is Barrot. I live in Ukrain. I lost my
                                            house. Can u help me? lore m ipsum dolor</p>
                                        <div class="main-adopt-button">
                                            <div class="adopt-button">
                                                <a href="www">Adopt</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        </div> `;
                   

                maindiv.innerHTML += div;

            }));
       
       