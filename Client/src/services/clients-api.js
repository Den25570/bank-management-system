import axios from 'axios'

const getClients = async () => {
    let result = await axios.get(`https://${process.env.VUE_APP_API_ENDPOINT}/clients/all`, {
        'Content-Type': 'application/json'
    })
    result.data.forEach(client => {
        client.birthday = client.birthday.substring(0, client.birthday.indexOf("T"));
        client.passportIssueDate = client.passportIssueDate.substring(0, client.passportIssueDate.indexOf("T"));
    });
    return result.data;
}

const searchClientsByPassport = async (passport) => {
    let result = await axios.get(`https://${process.env.VUE_APP_API_ENDPOINT}/clients/all/passport?passportIdNumber=${passport}`, {
        'Content-Type': 'application/json'
    })
    return result.data;
}

const getClient = async (id) => {
    let result = await axios.get(`https://${process.env.VUE_APP_API_ENDPOINT}/clients?id=${id}`, {
        'Content-Type': 'application/json'
    })
    result.data.birthday = result.data.birthday.substring(0, result.data.birthday.indexOf("T"));
    result.data.passportIssueDate = result.data.passportIssueDate.substring(0, result.data.passportIssueDate.indexOf("T"));
    console.log(result)
    return result.data;
}

const deleteClient = async (id) => {
    let result = await axios.delete(`https://${process.env.VUE_APP_API_ENDPOINT}/clients?id=${id}`, {
        'Content-Type': 'application/json'
    })
    console.log(result)
    return result.data;
}

const updateClient = async (data) => {
    let result = await axios.put(`https://${process.env.VUE_APP_API_ENDPOINT}/clients`, data, {
        'Content-Type': 'application/json'
    })
    console.log(result)
    return result.data;
}

const createClient = async (data) => {
    let result = await axios.post(`https://${process.env.VUE_APP_API_ENDPOINT}/clients`, data, {
        'Content-Type': 'application/json'
    })
    console.log(result)
    return result.data;
}

export {
    getClients,
    getClient,
    deleteClient,
    updateClient,
    createClient,
    searchClientsByPassport
}