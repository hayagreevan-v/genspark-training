const users =[
    {Name: "Hex", Age :24, Email: "hex@mail.com"},
    {Name: "Ramu", Age :25, Email: "ramu@mail.com"},
    {Name: "Somu", Age :26, Email: "sommu@mail.com"}
];

const callbackBtn = document.querySelector("#callbackBtn");
const asyncAwaitBtn = document.querySelector("#asyncAwaitBtn");
const promiseBtn = document.querySelector("#promiseBtn");
const data = document.querySelector(".data");

const getUsers = () => {
    users.forEach((user)=>{
        console.log(user);
        let p = document.createElement("p");
        p.innerHTML = `${user.Name}\t${user.Age}\t${user.Email}`
        data.appendChild(p);
    })
}

async function delay(n){
    await new Promise((resolve)=> setTimeout(resolve,n));
}

asyncAwaitBtn.addEventListener("click",async ()=>{
    await delay(2000);
    getUsers();
    console.log("Completed");
});


callbackBtn.addEventListener("click",async ()=>{
    setTimeout(()=>{
        getUsers();
        console.log("Completed");
    },2000);
})

promiseBtn.addEventListener("click",async ()=>{
    new Promise((resolve,reject)=>{
        if(users == null || users == undefined) reject();
        getUsers();
        resolve();
    }).then(()=>{
        console.log("completed");
    })
    .catch((ex) =>{
        console.log("Error");
    })
})