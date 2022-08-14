let searchBar=document.querySelector('.form-control');
let contactNames= document.querySelectorAll('.contact-name');
searchBar.addEventListener('keyup', function(){
    contactNames.forEach(name=>{


        let contact=name.parentNode.parentNode.parentNode;
        if (name.innerHTML.toLowerCase().includes(searchBar.value.toLowerCase())) {
            contact.style='display:block';
        }
        else{
            contact.style='display:none';
        }
    })
})