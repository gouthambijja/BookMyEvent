
const Persist = () => {
    return localStorage.getItem("persist");
};
const setPersist = () =>{
    localStorage.setItem("persist",true);
}
const unsetPersist = () =>{
    localStorage.setItem("persist",false);
}
export {setPersist,unsetPersist};
export default Persist;