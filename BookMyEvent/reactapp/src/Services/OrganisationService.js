import axios from "axios";
import Axios from "../Api/Axios";

const getAllOrganisations = async () => {
    const response = await Axios.get(`/api/Organisation`,
        {
            headers: { 'Content-Type': 'application/json' },
            withCredentials: true
        }
    );
    return response.data;
}
export { getAllOrganisations };
