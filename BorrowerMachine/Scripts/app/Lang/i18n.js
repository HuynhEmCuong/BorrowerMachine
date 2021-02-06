import Vue from 'vue'
import VueI18n from 'vue-i18n'

import vnMessage from '../lang/vn.json';
import enMessage from '../lang/en.json';
import twMessage from '../lang/tw.json';


Vue.use(VueI18n);

const messages = {
    vn: vnMessage,
    en: enMessage,
    zh: twMessage,
};

const i18n = new VueI18n({
    messages,
    fallbackLocale: 'vn',
    silentFallbackWarn: true
});



export default i18n
