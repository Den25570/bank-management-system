<template>
    <div>
        <div>
            <b-row class="my-1">
                <b-col sm="3"><label>Имя</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-input v-model="clientLocal.firstname" :state="firstnameState"></b-form-input></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Фамилия</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-input v-model="clientLocal.lastname" :state="lastnameState"></b-form-input></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Отчество</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-input v-model="clientLocal.middlename" :state="middlenameState"></b-form-input></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>День рождения</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-input v-model="clientLocal.birthday" :state="birthdayState" type="date"></b-form-input></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Пол</label></b-col>
                <b-col sm="9" class="d-flex align-items-center">
                    <b-form-radio class="d-flex flex-row-reverse mx-2" v-model="clientLocal.sex" :value="false">М</b-form-radio>
                    <b-form-radio class="d-flex flex-row-reverse" v-model="clientLocal.sex" :value="true">Ж</b-form-radio>
                </b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Идентификационный номер</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-input v-model="clientLocal.passportIdNumber" :state="passportIdNumberState"></b-form-input></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Номер паспорта</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-input v-model="clientLocal.passportNumber" :state="passportNumberState"></b-form-input></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Серия паспорта</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-input v-model="clientLocal.passportSeries" :state="passportSeriesState"></b-form-input></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Издатель паспорта</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-input v-model="clientLocal.passportIssuer" :state="passportIssuerState"></b-form-input></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Дата выпуска паспорта</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-input v-model="clientLocal.passportIssueDate" :state="passportIssueDateState" type="date"></b-form-input></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Место рождения</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-input v-model="clientLocal.birthPlace" :state="birthPlaceState"></b-form-input></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Город проживания</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-select :options="cities" v-model="clientLocal.residenceCityId" :state="residenceCityIdState"></b-form-select></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Адрес проживания</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-input v-model="clientLocal.residenceAddress" :state="residenceAddressState"></b-form-input></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Домашний номер</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-input v-model="clientLocal.phoneNumberStationary" :state="phoneNumberStationaryState"></b-form-input></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Мобильный номер</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-input v-model="clientLocal.phoneNumberMobile" :state="phoneNumberMobileState"></b-form-input></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Email</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-input v-model="clientLocal.email" :state="emailState" type="email"></b-form-input></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Семейный статус</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-select :options="familyStatuses" v-model="clientLocal.familyStatusId" :state="familyStatusIdState"></b-form-select></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Гражданство</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-select :options="citizenships" v-model="clientLocal.citizenshipId" :state="citizenshipIdState"></b-form-select></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Город регистрации</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-select :options="cities" v-model="clientLocal.registrationCityId" :state="registrationCityIdState"></b-form-select></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Адрес регистрации</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-input v-model="clientLocal.registrationAddress" :state="registrationAddressState"></b-form-input></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Инвалидность</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-select :options="disabilities" v-model="clientLocal.disabilityId" :state="disabilityIdState"></b-form-select></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Пенсионер</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-checkbox v-model="clientLocal.isRetired" :state="isRetiredState"></b-form-checkbox></b-col>
            </b-row>
            <b-row class="my-1">
                <b-col sm="3"><label>Месячный доход</label></b-col>
                <b-col sm="9" class="d-flex align-items-center"><b-form-input v-model="clientLocal.mounthlyIncome" :state="mounthlyIncomeState" type="number"></b-form-input></b-col>
            </b-row>
        </div>
        <div class="d-flex justify-content-end mt-4">
            <template v-if="isEdit">
                <b-button class="mx-2" variant="success" @click="UpdateClient" :disabled="triedUpdateOrCreate && !state">Обновить</b-button>
                <b-button variant="danger" @click="DeleteClient">Удалить</b-button>
            </template>
            <template v-else>
                <b-button variant="success" @click="AddClient" :disabled="triedUpdateOrCreate && !state">Добавить</b-button>
            </template>
        </div>
    </div>
</template>

<script>
import {updateClient, deleteClient, createClient} from '../../services/api.js'
import {Cities, Citizenships, Disabilities, FamilyStatuses} from '../../data/data.js'
export default {
    name: "ClientEdit",
    props: {
        client: {
            default: {}
        },
        isEdit: {
            type: Boolean,
            default: true,
        }
    },
    data() {
        return {
            clientLocal: {},
            triedUpdateOrCreate: false,
            cities: Cities.map(c => {return {value: c.Id, text: c.Name}}),
            citizenships: Citizenships.map(c => {return {value: c.Id, text: c.Name}}),
            disabilities: Disabilities.map(c => {return {value: c.Id, text: c.Name}}),
            familyStatuses: FamilyStatuses.map(c => {return {value: c.Id, text: c.Name}}),
        }
    },
        computed: {
            firstnameState() {
                return this.triedUpdateOrCreate ? /^([а-яА-Я]|[a-zA-Z])+$/.test(this.clientLocal.firstname): null
            },
            lastnameState() {
                return this.triedUpdateOrCreate ? /^([а-яА-Я]|[a-zA-Z])+$/.test(this.clientLocal.lastname): null
            },
            middlenameState() {
                return this.triedUpdateOrCreate ? /^([а-яА-Я]|[a-zA-Z])+$/.test(this.clientLocal.middlename): null
            },
            birthdayState() {
                return this.triedUpdateOrCreate ? !!this.clientLocal.birthday: null
            },
            sexState() {
                return this.triedUpdateOrCreate ? this.clientLocal.sex !== null || this.clientLocal.sex !== undefined: null
            },
            passportIdNumberState() {
                return this.triedUpdateOrCreate ? /^[0-9]{7}[а-яА-Я][0-9]{3}[а-яА-Я]{2}[0-9]$/.test(this.clientLocal.passportIdNumber): null
            },
            passportNumberState() {
                return this.triedUpdateOrCreate ? /^[0-9]{7}$/.test(this.clientLocal.passportNumber): null
            },
            passportSeriesState() {
                return this.triedUpdateOrCreate ? /^[а-яА-Я]{2}$/.test(this.clientLocal.passportSeries): null
            },
            passportIssuerState() {
                return this.triedUpdateOrCreate ? !!this.clientLocal.passportIssuer: null
            },
            passportIssueDateState() {
                return this.triedUpdateOrCreate ? !!this.clientLocal.passportIssueDate: null
            },
            birthPlaceState() {
                return this.triedUpdateOrCreate ? !!this.clientLocal.birthPlace: null
            },
            residenceCityIdState() {
                return this.triedUpdateOrCreate ? !!this.clientLocal.residenceCityId: null
            },
            registrationCityIdState() {
                return this.triedUpdateOrCreate ? !!this.clientLocal.registrationCityId: null
            },
            residenceAddressState() {
                return this.triedUpdateOrCreate ? !!this.clientLocal.residenceAddress: null
            },
            phoneNumberStationaryState() {
                return this.triedUpdateOrCreate ? /^[+]?[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\\./0-9]{8,9}$/.test(this.clientLocal.phoneNumberStationary): null
            },
            phoneNumberMobileState() {
                return this.triedUpdateOrCreate ? /^[+]?[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\\./0-9]{5,9}$/.test(this.clientLocal.phoneNumberMobile): null
            },
            emailState() {
                return this.triedUpdateOrCreate ? /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(this.clientLocal.email): null
            },
            familyStatusIdState() {
                return this.triedUpdateOrCreate ? !!this.clientLocal.familyStatusId: null
            },
            citizenshipIdState() {
                return this.triedUpdateOrCreate ? !!this.clientLocal.citizenshipId: null
            },
            registrationAddressState() {
                return this.triedUpdateOrCreate ? !!this.clientLocal.registrationAddress: null
            },
            disabilityIdState() {
                return this.triedUpdateOrCreate ? !!this.clientLocal.disabilityId: null
            },
            isRetiredState() {
                return this.triedUpdateOrCreate ? true: null
            },
            mounthlyIncomeState() {
                return this.triedUpdateOrCreate ? true: null
            },
            state() {
                return this.firstnameState && this.lastnameState  && this.middlenameState  && this.birthdayState  && 
                   this.sexState  && this.passportIdNumberState  && this.passportNumberState && this.passportSeriesState  && 
                   this.passportIssuerState  && this.passportIssueDateState  && this.birthPlaceState && this.residenceCityIdState  && 
                   this.registrationCityIdState  && this.phoneNumberStationaryState  && this.phoneNumberMobileState && this.emailState  && 
                   this.familyStatusIdState  && this.citizenshipIdState  && this.registrationAddressState && this.disabilityIdState  && this.isRetiredState && this.mounthlyIncomeState
            }
        },
    mounted() {
        this.clientLocal = this.client ? this.client : {}
    },
    methods: {
        CheckState() {
            
        },
        AddClient() {
            this.triedUpdateOrCreate = true;
            if (this.state) {
                createClient(this.clientLocal).then(result => {
                    alert(result.message)
                    this.$forceUpdate();
                })
            }

        },
        UpdateClient() {
            this.triedUpdateOrCreate = true;
            if (this.state) {
                updateClient(this.clientLocal).then(result => {
                    alert(result.message)
                })
            }
        },
        DeleteClient() {
            deleteClient(this.clientLocal.id).then(result => {
                alert(result.message)
                this.$forceUpdate();
            })
        }
    }
}
</script>

<style scoped>
.custom-control-input {
    margin: 5px !important;
}
.custom-radio {
    align-items: baseline;
}
label {
    vertical-align: middle;
    width: 100%;
    line-height: 20px;
}
</style>