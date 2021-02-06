<template>

  <div class="modal fade" id="modalDevice" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title"> <i class="fa fa-television"></i>{{$t('device.AddDevice')}}</h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <div class="form-group row ">
            <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('common.DeviceName')}}</label>
            <div class="col-sm-8">
              <input type="text" v-model="nameDevice" class="form-control">
            </div>
          </div>
          <div class="form-group row ">
            <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('common.Category')}}</label>
            <div class="col-sm-8">
              <Select2 v-model="idCate" :options="listCategoryDevice">

              </Select2>
            </div>
          </div>
          <div class="form-group row ">
            <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('common.Quantity')}}</label>
            <div class="col-sm-8">
              <input type="number" v-model="quantity" class="form-control">
            </div>
          </div>

        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-danger" @click="AddDevice">{{$t('common.Add')}}</button>
          <button type="button" class="btn btn-secondary" data-dismiss="modal">{{$t('common.Close')}}</button>
        </div>
      </div>
    </div>
  </div>

</template>
<script>
  import axios from 'axios'
  import Select2 from 'v-select2-component'
  import AddComponent from './AddComponent.vue'
  export default {
    components: {
      Select2,
      AddComponent
    },
    data() {
      return {
        isNameCategory:'',
        listCategoryDevice: [],
        idCate: 0,
        nameDevice: '',
        quantity: 1
      }
    },
    methods: {
      GetListCate() {
        axios.get('/home/GetListCategory').then(res => {
          let data = res.data;
          this.listCategoryDevice = data.map(x => {
             return { id: x.ID, text: (this.isNameCategory == 'vn' ? x.NameCate : (this.isNameCategory == 'zh' ? x.Name_TW : x.Name_EN)) }
          })
          this.listCategoryDevice.unshift({ id: 0, text: this.langIndex("common.ListCateDevice") })
        }).catch(e => { console.log(e) })
      },
      AddDevice() {
   
        var userName = this.$store.state.user;
        if (this.CheckData()) {
          axios.post('/Device/InsertDevice', {
            name: this.nameDevice, idCate: this.idCate, quantity: this.quantity, nameCreate: userName.FullName
          }).then(res => {
            NotifySuccess(this.langIndex("alert.Device_Add"));
            this.$store.dispatch('getListDevice', 0);
            $("#modalDevice").modal("hide");

          })
        }

      },
      CheckData() {
        if (this.idCate == 0) {
          NotifyDanger(this.langIndex("alert.Device_Category"))
          return false;
        }
        else if (this.nameDevice == '' || this.nameDevice == null) {
          NotifyDanger(this.langIndex("alert.Device_Name"))
          return false;
        }
        else if (this.quantity == 0) {
          NotifyDanger(this.langIndex("alert.Device_Quantity"))
          return false;
        }
        else {
          return true
        }
      }
    },
    created() {
      this.GetListCate();
       if (this.$cookies.get("locate") == '' || this.$cookies.get("locate") == null) {
        this.isNameCategory = 'vn';
      }
      else this.isNameCategory = this.$cookies.get("locate");
    }
  }
</script>
<style>
</style>
