import { Outlet } from "react-router-dom";
import { useState, useEffect } from "react";
import useRefreshToken from "../Hooks/RefreshToken";
import { useSelector } from "react-redux";
import usePersist from "../Hooks/Persist";

const PersistLogin = () => {
    const [isLoading, setIsLoading] = useState(true);
    const refresh = useRefreshToken();
    const auth = useSelector(store => store.auth)
    let persist = usePersist();
    useEffect(() => {
        let isMounted = true;
        const verifyRefreshToken = async () => {
            try {
                //console.log('hey');
                await refresh();
            }
            catch (err) {
                console.error(err);
            }
            finally {
                isMounted && setIsLoading(false);
            }
        }

        // persist added here AFTER tutorial video
        // Avoids unwanted call to verifyRefreshToken
        !auth?.accessToken && persist ? verifyRefreshToken() : setIsLoading(false);

        return () => isMounted = false;
    }, [])

    useEffect(() => {
        //console.log(`isLoading: ${isLoading}`)
        //console.log(`aT: ${JSON.stringify(auth?.accessToken)}`)
    }, [isLoading])

    return (
        <>
            {!persist
                ? <Outlet />
                : isLoading
                    ? <p>Loading...</p>
                    : <Outlet />
            }
        </>
    )
}

export default PersistLogin