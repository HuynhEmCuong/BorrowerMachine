<template>
  <div>
    <div class="row list-option" :class="{overMainCate:isShowLoading}">
      <div class="over-loading">
        <div class="over-img">
          <img src="/Images/loading.gif" />
        </div>

      </div>
      <div class="col-md-3 col-12 mb-2 list-item" v-for="cate in listCategory">
        <div class="item">
          <button class="btn btn-danger name-cate" @click="SetLocalStore(cate)">
            {{isNameCategory=='vn' ? cate.NameCate:(isNameCategory=='zh'? cate.Name_TW :cate.Name_EN)}}
          </button>
          <span class="amount">
            <span class="pull-right">
              {{cate.SumQuantityInventory==null?0:cate.SumQuantityInventory}} / {{cate.SumQuantityBorrow + cate.SumQuantityInventory}}
            </span>
          </span>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
  import axios from 'axios';
  import ModalAddBorrowerComponent from './ModalAddBorrowerComponent'
  export default {
    components: {
      ModalAddBorrowerComponent
    },
    data() {
      return {
        listCategory: [],
        isShowLoading: true,
        isNameCategory: ''
      }
    },
    methods: {
      GetListCate() {
        this.isShowLoading = true;
        axios.get('/home/GetListCategory').then(res => {
          debugger;
          let data = res.data;
          this.listCategory = data;
       
          this.isShowLoading = false;
        }).catch(e => { console.log(e) })
      },
      SetLocalStore(cate) {
        localStorage.setItem("cate", JSON.stringify(cate));
        $(".modalAdd").modal("show");
      }
    },
    created() {
      this.GetListCate();
      debugger;
      if (this.$cookies.get("locate") == '' || this.$cookies.get("locate") == null) {
        this.isNameCategory = 'vn';
      }
      else this.isNameCategory = this.$cookies.get("locate");
      //Theo dõi xem nếu có người mượn hoặc người trả thì sẽ load lại cate
      this.$store.subscribe((mutation, state) => {
        if (mutation.type === 'SET_BORROWER_LIST') {
          this.GetListCate();
        }
      })
    }
  }
</script>
<style>
</style>
