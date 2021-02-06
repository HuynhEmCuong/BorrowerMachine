import axios from 'axios';
export const commonMixin = {
    data() {
        return {

        }
    },
    methods: {
        ConvertDate(date, format = "dd/mm/yyyy") {
            if (date == "" || date == null) {
                return "";
            }
            var nowDate = new Date(parseInt(date.substr(6)));
            return nowDate.format(format);
        },
        langIndex(key) {
            return this.$i18n.t(key);
        },
    },
    created() {

    }
}
