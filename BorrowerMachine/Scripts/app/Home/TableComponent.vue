<template>
  <div>
    <div class="update-panel">
      <div class="row">
        <div class="col-md-6">
          <div class="panel-left">
            <i class="fa fa-list"></i>
            {{$t("homepage.ListBorrowMachine")}}
          </div>
        </div>
        <div class="col-md-6 ">
          <div class="row">
            <div class="col-4">

            </div>
            <div class="col-8 ">
              <div class="row">
                <div class="col-8">
                  <Select2 :options="listCategoryDevice" v-model="idCate">

                  </Select2>
                </div>
                <div class="col-4">
                  <div class="pull-right">

                    <a style="width:117px" href="/Home/ExportBorrower" class="btn btn-success"><i class="fa fa-file-excel-o"></i> {{$t('common.ExportExcel')}}</a>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="panel-table" :class="{overMain:isShowLoading}">

      <div class="over-loading">
        <div class="over-img">
          <img src="/Images/loading.gif" />
        </div>

      </div>


      <table class="table table-bordered table-hover text-center">
        <thead>
          <tr>
            <th scope="col"><i class="fa fa-arrow-circle-o-down"></i></th>
            <th scope="col">{{$t('common.DeviceName')}}</th>
            <th scope="col">{{$t('common.Quantity')}}</th>
            <th scope="col">{{$t('homepage.Department_Home')}}/ {{$t('common.Borrower')}}</th>
            <th scope="col">{{$t('common.BorrowDate')}}</th>
            <th scope="col">{{$t('common.Actions')}}</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(borrow,index) in listBorrower ">
            <th scope="row">{{index+1 }}</th>
            <td>{{borrow.DevcieName}}</td>
            <td>{{borrow.Quantity}}</td>
            <td> <b>{{borrow.DepartName}}</b> - {{borrow.EmployName}} </td>
            <td>{{ConvertDate(borrow.DateBorrow,"dd/mm/yyyy HH:MM")}}</td>
            <td>
              <button class="btn btn-success form-group form-control" @click="ShowModalView(borrow)"><i class="fa fa-search-plus"></i> {{$t('common.View')}}</button>
              <button v-if="borrow.Status==2" class="btn btn-warning form-group form-control" @click="ShowModalReturn(borrow)">
                <i class="fa fa-refresh fa-spin"></i>
                {{$t('homepage.Wait')}}
              </button>
              <button v-else class="btn btn-warning form-group form-control" @click="ShowModalReturn(borrow)"> <i class="fa fa-share-square mr-2"></i>{{$t('homepage.Return')}}</button>
            </td>
          </tr>
        </tbody>
      </table>
      <!-- Modal View -->
      <div class="modal fade " id="modalView" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title"> <i class="fa fa-television"></i> {{$t('common.View')}}</h5>
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
              <div class="form-group row ">
                <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('homepage.Member')}}</label>
                <div class="col-sm-8">
                  <input type="text" v-model="returnBorrower.Member" disabled class="form-control-plaintext">
                </div>
              </div>
              <div class="form-group row ">
                <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('common.DeviceName')}}</label>
                <div class="col-sm-8">
                  <input type="text" v-model="returnBorrower.DevcieName" disabled class="form-control-plaintext">
                </div>
              </div>
              <div class="form-group row ">
                <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('common.BorrowDate')}}</label>
                <div class="col-sm-8">
                  <input type="text" v-model="returnBorrower.BorroweDate" disabled class="form-control-plaintext">
                </div>
              </div>
              <div class="form-group row ">
                <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('homepage.LongTime')}}</label>
                <div class="col-sm-8">
                  <input type="text" v-model="returnBorrower.LongTime" disabled class="form-control-plaintext">
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Modal Return -->
      <div class="modal fade " id="modalReturn" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title"> <i class="fa fa-television"></i> {{$t('homepage.Return')}}</h5>
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
              <div class="form-group row ">
                <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('common.DeviceName')}}</label>
                <div class="col-sm-8">
                  <input type="text" v-model="returnBorrower.DevcieName" disabled class="form-control">
                </div>
              </div>
              <div class="form-group row ">
                <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('homepage.Department_Home')}}</label>
                <div class="col-sm-8">
                  <input type="text" disabled class="form-control" v-model="returnBorrower.DepartName">
                </div>
              </div>
              <div class="form-group row ">
                <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('homepage.Member')}}</label>
                <div class="col-sm-8">
                  <input type="text" disabled v-model="returnBorrower.Member" class="form-control">
                </div>
              </div>
              <div class="form-group row ">
                <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('common.BorrowDate')}}</label>
                <div class="col-sm-8">
                  <input type="text" v-model="returnBorrower.BorroweDate" disabled class="form-control">
                </div>
              </div>
              <div class="form-group row ">
                <label for="inputEmail3" class="col-sm-4 col-form-label"> {{$t('common.DateReturn')}}</label>
                <div class="col-sm-8">
                  <!--<datepicker name="date"
                              input-class="form-control"
                              placeholder="Ngày trả"
                              format="dd/MM/yyyy"
                              v-model="returnBorrower.ModifierDateReturn">
                  </datepicker>-->
                  <date-picker v-model="returnBorrower.ModifierDateReturn" :config="options"></date-picker>
                </div>
              </div>
              <div class="form-group row ">
                <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('common.ReturnBy')}}</label>
                <div class="col-sm-8">
                  <div class="row">
                    <div class="col-4">
                      <input type="text" v-model="returnBorrower.EmployNumberReturn" class="form-control col">
                    </div>
                    <div class="col-8">
                      <input type="text" v-model="returnBorrower.EmployNameReturn" class="form-control col">
                    </div>
                  </div>
                </div>
              </div>
              <div class="form-group row " v-show="isCheckUser">
                <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('homepage.AcceptBy')}}</label>
                <div class="col-sm-8">
                  <div class="row">
                    <div class="col-4">
                      <input type="text" v-model="returnBorrower.AcceptByNumber" class="form-control col">
                    </div>
                    <div class="col-8">
                      <input type="text" v-model="returnBorrower.AcceptByName" class="form-control col">
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-success" @click="SubmitReturn">Submit</button>
              <button type="button" class="btn btn-danger" data-dismiss="modal">Cancel</button>
            </div>
          </div>
        </div>
      </div>
    </div>

  </div>
</template>
<script>
  import axios from 'axios'
  import Select2 from 'v-select2-component'
  import Datepicker from 'vuejs-datepicker'
  import datePicker from 'vue-bootstrap-datetimepicker';
  import 'pc-bootstrap4-datetimepicker/build/css/bootstrap-datetimepicker.css';
  import { mapGetters } from 'vuex'
  export default {
    props: ['isLoad'],
    components: {
      Select2,
      Datepicker,
      datePicker
    },
    data() {
      return {
        isNameCategory: '',
        isReturn: true,
        isCheckUser: false,
        dateBorrow: '',
        idDepart: 0,
        idCate: 0,
        idDevice: 0,
        listCategoryDevice: [],
        listDevice: [{ id: 0, text: this.langIndex("common.ListDevice") }],
        listDepart: [],
        returnBorrower: {
          DevcieName: '',
          DepartName: '',
          Member: '',
          DateReturn: '',
          BorroweDate: '',
          ModifierDateReturn: '',
          EmployNumberReturn: '',
          EmployNameReturn: '',
          AcceptByNumber: '',
          AcceptByName: '',
          LongTime: '',
          Status: 1,
          Quantity: 1
        },
        options: {
          locale: 'vi',
          sideBySide: true,
          format: 'YYYY/MM/DD HH:mm',
          useCurrent: true,
          calendarWeeks: true
        }
      }
    },
    computed: {
      ...mapGetters(["listBorrower", "isShowLoading"])
    },
    methods: {
      GetAllBorrower(id) {
        this.$store.dispatch('getListBorrower', id);
      },
      GetListCate() {
        axios.get('/home/GetListCategory').then(res => {
          let data = res.data;
          this.listCategoryDevice = data.map(x => {
            return { id: x.ID, text: (this.isNameCategory == 'vn' ? x.NameCate : (this.isNameCategory == 'zh' ? x.Name_TW : x.Name_EN)) }
          })
          this.listCategoryDevice.unshift({ id: 0, text: this.langIndex('common.ListCateDevice') })
        }).catch(e => { console.log(e) })
      },
      GetListDevice(id) {
        axios.post('/home/GetDevice', { id: id }).then(res => {
          let data = res.data;
          this.listDevice = data.map(x => {
            return { id: x.Id, text: x.Name_Vi }
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
      ShowModalView(borrow) {
        this.returnBorrower = borrow;
        this.returnBorrower.BorroweDate = this.ConvertDate(borrow.DateBorrow, "dd/mm/yyyy HH:MM");

        this.returnBorrower.LongTime = (borrow.LongTime == false) ? "NO" : "YES";
        this.returnBorrower.Member = borrow.EmployName + '-' + borrow.EmployBorrower + '-' + borrow.DepartName;
        $("#modalView").modal("show");
      },
      ShowModalReturn(borrow) {

        this.returnBorrower.Member = "";
        this.checkUser();
        this.returnBorrower = borrow;

        this.returnBorrower.Member = borrow.EmployName + '-' + borrow.EmployBorrower;
        this.returnBorrower.BorroweDate = this.ConvertDate(borrow.DateBorrow, "dd/mm/yyyy HH:MM");
        this.returnBorrower.AcceptByName = borrow.EmployNameAccept;
        this.returnBorrower.AcceptByNumber = borrow.EmployNumberAccept;
        this.returnBorrower.EmployNumberReturn = borrow.EmployNumberReturn == null ? borrow.EmployBorrower : borrow.EmployNumberReturn;
        this.returnBorrower.EmployNameReturn = borrow.EmployNameReturn == null ? borrow.EmployName : borrow.EmployNameReturn;
        this.returnBorrower.ModifierDateReturn = borrow.DateReturn == null ? new Date() : this.ConvertDate(borrow.DateReturn, "mm/dd/yyyy");

        $("#modalReturn").modal("show");
      },
      checkUser() {
        var userData = this.$store.state.user;
        this.isCheckUser = userData == null ? false : true;
      },
      SubmitReturn() {
        this.returnBorrower.Status = this.isCheckUser == false ? 2 : 3;

        if (this.checkData()) {
          //Lưu lại người xác nhận vào bộ nhớ trình duyệt cho họ đỡ phải nhập lần sau
          var acceptBy = JSON.stringify({ number: this.returnBorrower.AcceptByNumber, name: this.returnBorrower.AcceptByName });
          localStorage.setItem("acceptBy", acceptBy);

          axios.post('/home/UpdateBorrower', { data: this.returnBorrower }).then(res => {
            console.log(res.data);
            this.returnBorrower.Status == 3 ? NotifySuccess(this.langIndex("alert.Accept_Success")) : NotifySuccess(this.langIndex("alert.Return_Success"));
            this.GetAllBorrower(this.idCate);
            $("#modalReturn").modal("hide");

          }).catch(e => { NotifyDanger(this.langIndex("alert.Login_ErrorSysten")) })
        }
      },
      checkData() {
        if (this.isCheckUser == false) {
          if (this.returnBorrower.EmployNumberReturn == "" || this.returnBorrower.EmployNumberReturn == null) {
            NotifyDanger(this.langIndex("alert.Return_Number"));
            return false;
          }
          else if (this.returnBorrower.EmployNameReturn == null || this.returnBorrower.EmployNameReturn == "") {
            NotifyDanger(this.langIndex("alert.Return_Name"))
            return false
          }
          else if (this.returnBorrower.ModifierDateReturn == "" || this.returnBorrower.ModifierDateReturn == null) {
            NotifyDanger(this.langIndex("alert.Retun_Date"))
            return false
          }
          else
            return true
        }
        else {
          if (this.returnBorrower.ModifierDateReturn == "" || this.returnBorrower.ModifierDateReturn == null) {
            NotifyDanger(this.langIndex("alert.Retun_Date"))
            return false
          }
          else if (this.returnBorrower.AcceptByNumber == null || this.returnBorrower.AcceptByNumber == "") {
            NotifyDanger(this.langIndex("alert.Accept_Num"))
            return false
          }
          else if (this.returnBorrower.AcceptByName == null || this.returnBorrower.AcceptByName == "") {
            NotifyDanger(this.langIndex("alert.Accept_Name"))
            return false
          }
          else
            return true
        }
      },
      checkLoad(isload) {
        if (isload == true) {
          this.GetAllBorrower(0)
        }
      }
    },
    mounted() {
      let self = this;
      $('#modalReturn').on('show.bs.modal', function (e) {

        //Kiểm tra nếu có accept rồi thì load lên lun
        var acceptBy = localStorage.getItem("acceptBy");
        if (acceptBy != null && acceptBy != "") {
          acceptBy = JSON.parse(acceptBy);
          self.returnBorrower.AcceptByNumber = acceptBy.number;
          self.returnBorrower.AcceptByName = acceptBy.name;
        }
      })
    },
    created() {
      this.GetAllBorrower(0);
      this.GetListCate();
      this.GetListDepart();
      this.returnBorrower.ModifierDateReturn = new Date();
      if (this.$cookies.get("locate") == '' || this.$cookies.get("locate") == null) {
        this.isNameCategory = 'vn';
      }
      else this.isNameCategory = this.$cookies.get("locate");
    },
    watch: {
      idCate: function (newVal) {
        this.GetAllBorrower(newVal)
        if (newVal == 0) {
          this.idDevice = 0;
          this.listDevice = [{ id: 0, text: 'Please choose device' }];
        }
      },
    },
  }
</script>
<style>
</style>
