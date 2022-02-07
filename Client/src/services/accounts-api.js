import axios from 'axios'

const getAccounts = async () => {
    let result = await axios.get(`https://${process.env.VUE_APP_API_ENDPOINT}/accounts`, {
        'Content-Type': 'application/json'
    })
    console.log(result)
    return result.data;
}

const endBankDay = async () => {
    let result = await axios.post(`https://${process.env.VUE_APP_API_ENDPOINT}/accounts`, {
        'Content-Type': 'application/json'
    })
    console.log(result)
    return result.data;
}

export {
    getAccounts, endBankDay
}