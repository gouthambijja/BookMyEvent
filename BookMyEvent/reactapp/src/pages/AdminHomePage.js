import React, { useEffect, useState } from 'react'
import useAxiosPrivate from '../Hooks/useAxiosPrivate';
import { useSelector } from 'react-redux';

const AdminHomePage = () => {
const [fakeString,setFakeString] = useState(['hey']);
const axiosPrivate = useAxiosPrivate();
const admin = useSelector(store => store.admin.profile)
  useEffect(()=>{
    console.log(admin);
    let isMounted = true;
     let getFakeString = async() =>{
      const fakeStringRes = await axiosPrivate.get("https://localhost:7101/api/user/getfakestring");
      setFakeString(prev => [...prev,fakeStringRes.data]);

    }
    getFakeString();
    return () => {
      isMounted = false;
  }
  },[])
  let getFakeString = async() =>{
    const fakeStringRes = await axiosPrivate.get("https://localhost:7101/api/user/getfakestring");
    setFakeString(prev => [...prev,fakeStringRes.data]);
    console.log(fakeStringRes);

  }
  return (
    <div>
      AdminHomePage<br></br>
      <button onClick={async()=>{await getFakeString()}}>getfakestring</button>
      {fakeString.map((str,i) => 
          <li key={i}>{str}</li>
        )}
    </div>
  )
}

export default AdminHomePage
