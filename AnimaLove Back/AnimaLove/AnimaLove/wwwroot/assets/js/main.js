
//active navbar menu
var bntcontainer=document.getElementById("bntcontainer");
var btns=bntcontainer.getElementsByClassName("pages-item");

for (let i = 0; i < btns.length; i++) {
  btns[i].addEventListener('click', function () {
    var currentBtn=document.getElementsByClassName("active-pages-item");
     currentBtn[0].className=currentBtn[0].className.replace(" active-pages-item");
 this.className+=" active-pages-item";

  })
  
}



//active shop category

var myBtnContainer=document.getElementById("myBtnContainer");
var categories=myBtnContainer.getElementsByClassName("categories-item");
for (let i = 0; i < categories.length; i++) {
  categories[i].addEventListener('click',function () {
    var currentCategory=document.getElementsByClassName("active-categories-item");
     currentCategory[0].className=currentCategory[0].className.replace(" active-categories-item");
this.className+=" active-categories-item";



  })
  
 }

//  //active shop category

// var myFirstBtnContainer=document.getElementById("myFirstBtnContainer");
// var categories=myFirstBtnContainer.getElementsByClassName("categories-item");
// for (let i = 0; i < categories.length; i++) {
//   categories[i].addEventListener('click',function () {
//     var currentCategory=document.getElementsByClassName("active-categories-item");
//      currentCategory[0].className=currentCategory[0].className.replace(" active-categories-item");
// this.className+=" active-categories-item";



//   })
  
//  }






 
  
  filterSelection("all")
function filterSelection(c) {
  var x, i;
  x = document.getElementsByClassName("filterDiv");
  if (c == "all") c = "";
  for (i = 0; i < x.length; i++) {
    RemoveClass(x[i], "show");
    if (x[i].className.indexOf(c) > -1) AddClass(x[i], "show");
  }
}

function AddClass(element, name) {
  var i, arr1, arr2;
  arr1 = element.className.split(" ");
  arr2 = name.split(" ");
  for (i = 0; i < arr2.length; i++) {
    if (arr1.indexOf(arr2[i]) == -1) {element.className += " " + arr2[i];}
  }
}

function RemoveClass(element, name) {
  var i, arr1, arr2;
  arr1 = element.className.split(" ");
  arr2 = name.split(" ");
  for (i = 0; i < arr2.length; i++) {
    while (arr1.indexOf(arr2[i]) > -1) {
      arr1.splice(arr1.indexOf(arr2[i]), 1);     
    }
  }
  element.className = arr1.join(" ");
}




//addlike
// var hearts=document.querySelectorAll(".fa-heart");

// var likecount=parseInt(document.querySelectorAll(".text")[0].firstChild.textContent);
// var originallikecount=likecount;
// function AddLike(){
  


// for (var i = 0; i < hearts.length; i++) {
//   likecount=parseInt(document.querySelectorAll(".text")[i].textContent);
//  originallikecount=likecount;
//  if (likecount==originallikecount) {
//   likecount+=1;
//    hearts[i].addEventListener("click",function() {
       
    
//         this.style.color = "#3e97a3";
//    this.parentNode.firstChild.nextElementSibling.textContent=`${likecount}`;
      
       
//     });
  

// }
// else{
//    likecount-=1;
//       hearts[i].addEventListener("click",function() {
       
//         this.style.color = "black";
//         this.parentNode.firstChild.nextElementSibling.textContent=`${originallikecount}`;

       
//     });

// }
// }
// }