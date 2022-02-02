<template>
    <b-container>
        <client-edit class="my-3 p-3 edit-component" :isEdit="false" :client="defaultClient"></client-edit>
    </b-container>
</template>

<script>
import ClientEdit from "../components/Clients/ClientEdit.vue"
export default {
    name: "CreateClient",
    components: {
        ClientEdit
    },
    data() {
        return {
            defaultClient: {
                "firstname": "Ли",
                "lastname": "Денис",
                "middlename": "Владиславович",
                "birthday": "2000-06-08",
                "sex": false,
                "passportIdNumber": "1234567В123РБ8",
                "passportNumber": "1234567",
                "passportSeries": "НВ",
                "passportIssuer": "Минский областной отдел",
                "passportIssueDate": "2020-01-01",
                "birthPlace": "г. Минск",
                "residenceCityId": -1,
                "registrationCityId": -1,
                "residenceAddress": "ул. Пушкина д. 21",
                "phoneNumberStationary": "+375291327089",
                "phoneNumberMobile": "+375291327089",
                "email": "den255700@gmail.com",
                "familyStatusId": -1,
                "citizenshipId": -1,
                "registrationAddress": "ул. Пушкина д. 21",
                "disabilityId": -1,
                "isRetired": false,
                "mounthlyIncome": 100
            }
        }
    },
}
</script>

<style scoped>
.edit-component {
    border: 1px solid lightgray;
}
</style>