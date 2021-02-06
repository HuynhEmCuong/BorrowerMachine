import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'
import VueCookies from 'vue-cookies';
import i18n from './Lang/i18n'

import { store } from './Store/store'
import { commonMixin } from './Mixin/commonMixin';
import { routes } from './routers'

Vue.mixin(commonMixin);
Vue.use(VueCookies);
Vue.use(VueRouter);
const router = new VueRouter(
    {
        routes
    }
);
const app = new Vue({
    i18n,
    el: '#app',
    store,
    router,
    render: h => h(App)
})

export default app
