import axios from 'axios'

const authAccount = async (number, pin) => {
    let result = await axios.post(`https://${process.env.VUE_APP_API_ENDPOINT}/terminal/auth`, {
        Number: number,
        PIN: pin,
    }, {
        'Content-Type': 'application/json'
    })
    console.log(result)
    return result.data;
}

const pay = async (accountId, sum) => {
    let result = await axios.post(`https://${process.env.VUE_APP_API_ENDPOINT}/terminal/pay`, {
        accountId: accountId,
        sum: sum,
    }, {
        'Content-Type': 'application/json'
    })
    console.log(result)
    return result.data;
}

export {
    authAccount, pay
}