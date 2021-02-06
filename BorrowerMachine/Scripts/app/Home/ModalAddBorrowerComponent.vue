<template>
  <div class="modal fade modalAdd" role="dialog" aria-labelledby="exampleModalCenterTitle">
    <div class="modal-dialog modal-lg" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title"> <i class="fa fa-plus-circle"></i> {{$t('homepage.Borrow_NewDevice')}}</h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <div class="form-group row">
            <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('homepage.Quick_Search')}}</label>
            <div class="col-sm-8">
              <autocomplete ref="autocomplete"
                            :placeholder="$t('homepage.Input_QuickSearch')"
                            :source="searchEmploy"
                            input-class="form-control"
                            @selected="selectedSearch">
              </autocomplete>
            </div>
          </div>
          <div class="form-group row important">
            <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('homepage.IdNumber')}}</label>
            <div class="col-sm-8">
              <input type="text" v-model="employNumBorrower" class="form-control">
            </div>
          </div>
          <div class="form-group row important">
            <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('homepage.FullName')}}</label>
            <div class="col-sm-8">
              <input type="text" v-model="employNameBorrower" class="form-control">
            </div>
          </div>
          <div class="form-group row">
            <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('homepage.Department_Home')}}</label>
            <div class="col-sm-8">
              <Select2 v-model="idDepart" :options="listDepart">

              </Select2>
            </div>
          </div>
          <div class="form-group row">
            <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('common.Category')}}</label>
            <div class="col-sm-8">
              <Select2 v-model="idCate" :options="listCategoryDevice" @change="GetListDevice($event)" @select="GetNameCate($event)">

              </Select2>
            </div>
          </div>
          <div class="form-group row important">
            <label for="inputEmail3" class="col-md-4 col-form-label">{{$t('common.DeviceName')}}</label>
            <div class="col-md-8">
              <Select2 v-model="idDevice" :options="listDevice" @select="GetNameDevice($event)">

              </Select2>
            </div>
          </div>
          <div class="form-group row ">
            <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('common.Quantity')}}</label>
            <div class="col-sm-8">
              <input type="number" v-model="quantity" class="form-control" min="1">
            </div>
          </div>
          <div class="form-group row ">
            <label class="form-check-label col-sm-4" for="exampleRadios1">
              {{$t('homepage.LongTime')}}
            </label>
            <div class="col-sm-8 checkbox">
              <input type="radio" id="longTime" value="true" v-model="longTime" />
              <label for="longTime" class="mr-3"> {{$t('common.Yes')}}</label>
              <input type="radio" id="shortTime" value="false" v-model="longTime" />
              <label for="shortTime">{{$t('common.No')}}</label>
            </div>

          </div>
          <div class="form-group row">
            <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('common.BorrowDate')}}</label>
            <div class="col-sm-8">
              <!--<datepicker name="date"
                          calendar-class="width-wrapper"
                          input-class="form-control "
                          placeholder="Ngày mượn"
                          format="dd/MM/yyyy hh:mm"
                          v-model="dateBorrow">
              </datepicker>-->

              <date-picker v-model="dateBorrow" :config="options"></date-picker>
            </div>

          </div>
          <div class="table-model">
            <table class="table table-bordered table-hover text-center">
              <thead>
                <tr>
                  <th scope="col"><i class="fa fa-arrow-circle-o-down"></i></th>
                  <th scope="col">{{$t('common.Category')}}</th>
                  <th scope="col">{{$t('common.DeviceName')}}</th>
                  <th scope="col">{{$t('common.Quantity')}}</th>
                  <th scope="col"><i class="fa fa-cog"></i></th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(device,index) in listAddBorrower ">
                  <td>{{index+1}}</td>
                  <td> {{device.NameCate}}</td>
                  <td>{{device.NameDevice}}</td>
                  <td>{{ device.Quantity}}</td>
                  <td>
                    <button class="btn btn-danger" @click="DeleteBorrower(index,device)">
                      <i class="fa fa-trash"></i>
                    </button>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-primary" @click="AddBorrower">{{$t('common.Add')}}</button>
          <button type="button" class="btn btn-success" @click="Submit">{{$t('common.Submit')}}</button>
          <button type="button" class="btn btn-danger" data-dismiss="modal">{{$t('common.Cancel')}}</button>

        </div>
      </div>
    </div>
  </div>
</template>
<script>
  import axios from 'axios'
  import Select2 from 'v-select2-component'
  import Datepicker from 'vuejs-datepicker'
  import Autocomplete from 'vuejs-auto-complete'
  import datePicker from 'vue-bootstrap-datetimepicker';
  import 'pc-bootstrap4-datetimepicker/build/css/bootstrap-datetimepicker.css';
  export default {
    components: {
      Select2,
      datePicker,
      Autocomplete,
    },
    data() {
      return {
        isNameCategory: '',
        amount: '',
        numberTotalDevice: 0,
        listAddBorrower: [],
        listCategoryDevice: [],
        listDevice: [{ id: 0, text: this.langIndex("common.ListDevice") }],
        listDepart: [],
        employNumBorrower: '',
        employNameBorrower: '',
        idCate: 0,
        nameCate: '',
        idDevice: 0,
        nameDevice: '',
        idDepart: 0,
        quantity: 1,
        longTime: false,
        dateBorrow: '',
        dataSearch: [],
        keysearch: 'aaa',
        options: {
          locale: 'vi',
          sideBySide: true,
          format: 'YYYY/MM/DD HH:mm',
          useCurrent: true,
          calendarWeeks: true
        }
      }
    },
    mounted() {
      let self = this;
      $('.modalAdd').on('show.bs.modal', function (e) {
        self.resetForm();

        var cate = localStorage.getItem("cate");
        if (cate != "" && cate != null) {
          cate = JSON.parse(cate);
          self.idCate = cate.ID;
          self.nameCate = cate.NameCate;
          self.GetListDevice(self.idCate);
        }

      })
      $('.modalAdd').on('hide.bs.modal', function (e) {
        localStorage.removeItem("cate");
        self.idCate = 0;
        self.nameCate = "";
        self.listDevice = [{ id: 0, text: 'Please choose device' }];
      })
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
      GetListDevice(id) {
        debugger;
        axios.post('/home/GetDevice', { id: id }).then(res => {
          let data = res.data;
          this.listDevice = data.map(x => {
            return { id: x.Id, text: x.Name_Vi + " ( " + this.amount + " : " + x.QuantityInventory + ")", quantity: x.QuantityInventory }
          });
          this.listDevice.unshift({ id: 0, text: this.langIndex("common.ListDevice") })

        }).catch(e => { console.log(e) })
      },
      GetListDepart() {
        axios.post('/home/GetListDepart').then(res => {
          let data = res.data;
          this.listDepart = data.map(x => {
            return { id: x.Id, text: x.Name }
          })
          this.listDepart.unshift({ id: 0, text: this.langIndex("common.ListDepart") })
        }).catch(e => { console.log(e) })
      },
      GetNameCate({ id, text }) {
        this.nameCate = text;
      },
      GetNameDevice({ id, text, quantity }) {
        this.nameDevice = text;

        this.numberTotalDevice = quantity;
      },
      AddBorrower() {
        var data = {
          EmployNumBorrower: this.employNumBorrower,
          EmployNameBorrower: this.employNameBorrower,
          IdCate: this.idCate,
          NameCate: this.nameCate,
          NameCate: this.nameCate,
          IdDevice: this.idDevice,
          NameDevice: this.nameDevice.substr(0, this.nameDevice.indexOf(" ")),
          IdDepart: this.idDepart,
          Quantity: this.quantity,
          LongTime: this.longTime,
          DateBorrow: typeof (this.dateBorrow) == "string" ? new Date(this.dateBorrow) : this.dateBorrow
        }
        if (this.CheckData(data)) {
          var isUpdateQuantityAdd = this.updateQuantityInselect(this.idDevice, this.quantity, "minutes");
          if (isUpdateQuantityAdd) {
            this.listAddBorrower.push(data);
            // this.resetForm();
          }
        }
      },
      searchEmploy(input) {
        return "/Home/SearchEmploy?keyword=" + input;
      },
      selectedSearch(value) {
        this.employNumBorrower = value.selectedObject.id;
        this.employNameBorrower = value.selectedObject.fullName;
        this.idDepart = value.selectedObject.deptId;
      },
      DeleteBorrower(index, itemDevice) {
        this.listAddBorrower.splice(index, 1);
        this.updateQuantityInselect(itemDevice.IdDevice, itemDevice.Quantity, "plus");
      },
      Submit() {

        if (this.listAddBorrower.length == 0) {
          NotifyDanger(this.langIndex("alert.Borrower_Submit"));
          return;
        }
        console.log("===");
        console.log(this.listAddBorrower);
        axios.post('/home/InsertBorrower', { data: this.listAddBorrower }).then(res => {

          this.listAddBorrower = [];
          NotifySuccess(this.langIndex("alert.Borrower_Success"));
          //Gọi load lại dữ liệu ngoài table
          this.$store.dispatch('getListBorrower', 0);
          $(".modalAdd").modal("hide");
        }).catch(e => { console.log(e) })
      },
      updateQuantityInselect(idDevice, quantity, type) {
        var deviceSelected = this.listDevice.find(item => item.id == idDevice);
        if (type == "minutes") {
          if (this.numberTotalDevice == 0) {
            NotifyWarning(this.langIndex("alert.Borrower_StockOut"));
            return false;
          }
          else if (this.numberTotalDevice < this.quantity) {
            NotifyWarning(this.langIndex("alert.Borrower_ErrorAddQuantity"));
            return false;
          }
          else {
            if (deviceSelected != null) {
              this.numberTotalDevice = deviceSelected.quantity - quantity;
            };
          }
        }
        else {
          this.numberTotalDevice = parseInt(deviceSelected.quantity) + parseInt(quantity);
        }
        var indexOfItem = this.listDevice.findIndex(item => item.id == this.idDevice);
        this.listDevice[indexOfItem].quantity = this.numberTotalDevice;
        this.listDevice[indexOfItem].text = this.nameDevice.replace(/: \d+/, ": " + this.numberTotalDevice);

        let listDevice = this.listDevice;
        this.listDevice = [];
        this.listDevice = this.listDevice.concat(listDevice);

        return true;

      },
      CheckData(data) {

        if (data.EmployNameBorrower == "") {
          NotifyDanger(this.langIndex("alert.Borrower_Name"));
          return false;
        }
        else if (data.EmployNumBorrower == "") {
          NotifyDanger(this.langIndex("alert.Borrower_Number"))
          return false;
        }
        else if (data.IdDepart == 0) {
          NotifyDanger(this.langIndex("alert.Borrower_Depart"))
          return false;
        }
        else if (data.IdDevice == 0) {
          NotifyDanger(this.langIndex("alert.Borrower_Device"))
          return false;
        }
        else if (data.Quantity == 0) {
          NotifyDanger(this.langIndex("alert.Borrowre_Quantity"))
          return false;
        }
        else if (data.DateBorrow == "") {
          NotifyDanger(this.langIndex("alert.Borrowre_Date"))
          return false;
        }
        else
          return true;

      },
      resetForm() {
        this.employNumBorrower = '';
        this.employNameBorrower = '';
        this.nameCate = '';
        this.idDevice = 0;
        this.nameDevice = '';
        this.idDepart = 0;
        this.quantity = 1;
        this.longTime = false;
        this.keysearch = 'ac';
      }
    },
    created() {
      this.GetListCate();
      this.GetListDepart();
      this.dateBorrow = new Date();
      this.amount = this.langIndex("homepage.Amuont");
      if (this.$cookies.get("locate") == '' || this.$cookies.get("locate") == null) {
        this.isNameCategory = 'vn';
      }
      else this.isNameCategory = this.$cookies.get("locate");
    }
  }
</script>
<style>

  span.select2-container {
    width: 100% !important;
  }

  .vdp-datepicker input {
    width: 100%;
  }
</style>
