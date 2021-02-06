import Vue from 'vue';
import Vuex from 'vuex';
import axios from "axios";
import app from '../index';
Vue.use(Vuex);

export const store = new Vuex.Store({
    state: {
        user: null,
        listBorrower: [],
        listDevice: [],
        isShowLoading: true,
        listDepart: []
    },
    getters: {
        listBorrower(state) {
            return state.listBorrower;
        },
        listDevice(state) {
            return state.listDevice;
        },
        listDepart(state) {
            return state.listDepart;
        },
        isShowLoading(state) {
            return state.isShowLoading;
        }
    },
    mutations: {
        SET_BORROWER_LIST(state, listBorrower) {
            state.listBorrower = listBorrower;
        },
        SET_DEVICE_LIST(state, listDevice) {
            state.listDevice = listDevice;
        },
        SET_DEPART_LIST(state, listDepart) {
            state.listDepart = listDepart;
        }
    },
    actions: {
        getListBorrower(context, id) {
            context.state.isShowLoading = true;

            axios.post('/home/GetListBorrower', { idCategory: id }).then(res => {
                context.commit("SET_BORROWER_LIST", res.data);
                context.state.isShowLoading = false;
            }).catch(e => {
                console.log(e);
            })
        },
        getListDevice(context, id) {
            axios.post('/Device/GetAllDevice', { IdCate: id }).then(res => {
                context.commit("SET_DEVICE_LIST", res.data)
              
            }).catch(e => { console.log(e) })
        },
        getListDepart(context) {
            axios.post('/Home/GetListDepart').then(res => {
                context.commit("SET_DEPART_LIST", res.data)
                console.log(res.data);
            }).catch(e => { console.log(e) })
        }
    }
})
