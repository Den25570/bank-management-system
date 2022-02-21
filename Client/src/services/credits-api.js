import axios from 'axios'

const getCredits = async () => {
    let result = await axios.get(`https://${process.env.VUE_APP_API_ENDPOINT}/credits`, {
        'Content-Type': 'application/json'
    })
    result.data.forEach(deposit => {
        deposit.startDate = deposit.startDate.substring(0, deposit.startDate.indexOf("T"));
        deposit.endDate = deposit.endDate.substring(0, deposit.endDate.indexOf("T"));
    });
    console.log(result)
    return result.data;
}

const getCreditCodes = async () => {
    let result = await axios.get(`https://${process.env.VUE_APP_API_ENDPOINT}/credits/codes`, {
        'Content-Type': 'application/json'
    })
    console.log(result)
    return result.data;
}

const createCredit = async (data) => {
    let result = await axios.post(`https://${process.env.VUE_APP_API_ENDPOINT}/credits`, data, {
        'Content-Type': 'application/json'
    })
    console.log(result)
    return result.data;
}

const revokeCredit = async (id) => {
    let result = await axios.post(`https://${process.env.VUE_APP_API_ENDPOINT}/credits/revoke/${id}`, {}, {
        'Content-Type': 'application/json'
    })
    console.log(result)
    return result.data;
}

export {
    getCredits, createCredit, revokeCredit, getCreditCodes
}

