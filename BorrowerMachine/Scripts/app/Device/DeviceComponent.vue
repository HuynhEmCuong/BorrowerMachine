<template>
  <div class="container">
    <div class="content">
      <div class="row">
        <div class="col-12" id="device">
          <div class="row page-titles">
            <div class="col-md-6 col-8 align-self-center">
              <h3 class="text-themecolor">  <i class="fa fa-arrow-circle-o-right"></i> &nbsp {{$t('device.AllDevice')}}</h3>
            </div>
            <div class="col-md-6 col-4">
              <div class="row">
                <div class="col-md-5">
                  <Select2 v-model="idCate" :options="listCategoryDevice">

                  </Select2>
                </div>

                <div class="col-md-7">
                  <div class="pull-right">
                    <button class="btn btn-primary" style="font-size:15px" data-toggle="modal" data-target="#modalDevice">
                      <i class="fa fa-plus-circle"></i> {{$t('device.AddDevice')}}
                    </button>
                    <a class="btn btn-success" href="/Device/ExportDevice">
                      <i class="fa fa-file-excel-o"></i>
                      {{$t('common.ExportExcel')}}
                    </a>
                  </div>

                </div>
              </div>
            </div>
            <add-component></add-component>
          </div>

          <div class="table-device">
            <table class="table table-bordered table-hover text-center">
              <thead>
                <tr>
                  <th scope="col">STT</th>
                  <th scope="col">{{$t('common.DeviceName')}}</th>
                  <th scope="col">{{$t('common.Category')}}</th>
                  <th scope="col">{{$t('device.InStock')}}</th>
                  <th scope="col">{{$t('common.Actions')}} </th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(device,index) in listDevice">
                  <th scope="row">{{index+1}}</th>
                  <td>{{device.NameDevice}}</td>
                  <td>{{device.NameCateDevice}}</td>
                  <td>{{device.Quantity}}</td>
                  <td>
                    <button class="btn btn-primary" @click="ShowViewModel(device)">
                      <i class="fa fa-eye"></i>
                      {{$t('common.View')}}
                    </button>
                    <button class="btn btn-warning" @click="ShowEditModel(device)">
                      <i class="fa fa-edit"></i>
                      {{$t('common.Edit')}}
                    </button>
                    <button class="btn btn-danger" @click="ShowModelDelete(device)">
                      <i class="fa fa-trash"></i>
                      {{$t('common.Delete')}}
                    </button>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>

          <div class="modal fade" id="modalTable" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title"> <i class="fa fa-television"></i> {{$t('device.DetailDevice')}} </h5>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <div class="form-group row ">
                    <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('common.DeviceName')}}</label>
                    <div class="col-sm-8">
                      <input type="text" class="form-control" v-model="deviceName" :disabled="isModal">
                    </div>
                  </div>
                  <div class="form-group row ">
                    <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('common.Category')}}</label>
                    <div class="col-sm-8">
                      <Select2 v-model="idCateModel" :options="listCategoryDevice" :disabled="isModal">

                      </Select2>
                    </div>
                  </div>
                  <div class="form-group row ">
                    <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('common.Quantity')}}</label>
                    <div class="col-sm-8">
                      <input type="number" min="1" class="form-control" v-model="deviceQuantity" :disabled="isModal">
                    </div>
                  </div>

                </div>
                <div class="modal-footer" v-if="!isModal">
                  <button type="button" class="btn btn-success" @click="Submit">{{$t('common.Submit')}}</button>
                  <button type="button" class="btn btn-danger" data-dismiss="modal">{{$t('common.Cancel')}}</button>
                </div>
              </div>
            </div>
          </div>

          <!-- Modal Delete -->
          <div class="modal fade" id="modalDelete" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title" id="exampleModalLabel">{{$t('device.DeleteDevice')}}</h5>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <p>
                    {{$t('device.DeviceQuestion')}} :
                    <b>
                      {{deviceCateName}}
                      -
                      {{deviceName}}
                    </b>
                  </p>
                </div>
                <div class="modal-footer">
                  <button type="button" class="btn btn-danger" @click="DeleteDevice">{{$t('common.Yes')}}</button>
                  <button type="button" class="btn btn-secondary" data-dismiss="modal">{{$t('common.No')}}</button>
                </div>
              </div>
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
  import AddComponent from './AddComponent.vue'
  import { mapGetters } from 'vuex'
  export default {
    components: {
      Select2,
      AddComponent
    },
    data() {
      return {
        listCategoryDevice: [],
        listAllDevice: [],
        idCate: 0,
        idCateModel: 0,
        isModal: true,
        deviceQuantity: 1,
        deviceName: '',
        idDevice: 0,
        isNameCategory: '',
        deviceCateName: ''
      }
    },
    computed: {
      ...mapGetters(["listDevice"])
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
      GetAllDevice(id) {
        //axios.post('/Device/GetAllDevice', { IdCate: id }).then(res => {
        //  this.listAllDevice = res.data;
        //  console.log(res.data);
        //}).catch(e => { console.log(e) })
        this.$store.dispatch('getListDevice', id);
      },
      ShowViewModel(device) {
        $("#modalTable").modal("show");
        this.isModal = true;
        this.idCateModel = device.IdCate;
        this.deviceName = device.NameDevice;
        this.deviceQuantity = device.Quantity;
      },
      ShowEditModel(device) {
        $("#modalTable").modal("show");
        this.isModal = false;
        this.idCateModel = device.IdCate;
        this.deviceName = device.NameDevice;
        this.deviceQuantity = device.Quantity;
        this.idDevice = device.Id;
      },
      ShowModelDelete(device) {
        $("#modalDelete").modal("show");
        this.deviceName = device.NameDevice;
        this.deviceCateName = device.NameCateDevice;
        this.idDevice = device.Id;
      },
      Submit() {
        var userName = this.$store.state.user;
        if (this.CheckData()) {
          axios.post('/Device/UpdateDevice', {
            id: this.idDevice, name: this.deviceName, idCate: this.idCateModel, quantity: this.deviceQuantity, nameUpdate: userName.FullName
          }).then(res => {
            console.log(res.data);
            $("#modalTable").modal("hide");
            NotifySuccess(this.langIndex("alert.Device_Edit"))
            this.GetAllDevice(this.idCate);
          }).catch(e => console.log(e))
        }
      },
      DeleteDevice() {
        axios.post('/Device/DeleteDevice', { id: this.idDevice }).then(res => {
          NotifySuccess(this.langIndex("alert.Device_Delete"))
          this.GetAllDevice(this.idCate);
          $("#modalDelete").modal("hide");
        }).catch(e => console.log(e))
      },
      CheckData() {
        debugger;
        if (this.idCateModel == 0) {
          NotifyDanger(this.langIndex("alert.Device_Category"))
          return false;
        }
        else if (this.deviceName == '' || this.deviceName == null) {
          NotifyDanger(this.langIndex("alert.Device_Name"))
          return false;
        }
        else if (this.deviceQuantity == 0) {
          NotifyDanger(this.langIndex("alert.Device_Quantity"))
          return false;
        }
        else {
          return true
        }
      }

    },
    created() {
      FadeLoadingBar();
      this.GetListCate();
      this.GetAllDevice(0);
      if (this.$cookies.get("locate") == '' || this.$cookies.get("locate") == null) {
        this.isNameCategory = 'vn';
      }
      else this.isNameCategory = this.$cookies.get("locate");
    },
    watch: {
      idCate(newval) {
        this.GetAllDevice(newval)
      }
    }
  }
</script>
<style>
  span.select2-container {
    width: 100% !important;
  }
</style>
