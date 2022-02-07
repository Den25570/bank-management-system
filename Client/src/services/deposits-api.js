import axios from 'axios'

const getDeposits = async () => {
    let result = await axios.get(`https://${process.env.VUE_APP_API_ENDPOINT}/deposits`, {
        'Content-Type': 'application/json'
    })
    result.data.forEach(deposit => {
        deposit.startDate = deposit.startDate.substring(0, deposit.startDate.indexOf("T"));
        deposit.endDate = deposit.endDate.substring(0, deposit.endDate.indexOf("T"));
    });
    console.log(result)
    return result.data;
}

const createDeposit = async (data) => {
    let result = await axios.post(`https://${process.env.VUE_APP_API_ENDPOINT}/deposits`, data, {
        'Content-Type': 'application/json'
    })
    console.log(result)
    return result.data;
}

const revokeDeposit = async (id) => {
    let result = await axios.post(`https://${process.env.VUE_APP_API_ENDPOINT}/deposits/revoke/${id}`, {}, {
        'Content-Type': 'application/json'
    })
    console.log(result)
    return result.data;
}

export {
    getDeposits, createDeposit, revokeDeposit
}

