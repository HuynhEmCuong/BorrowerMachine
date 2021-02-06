<template>
  <div class="container">
    <div class="content">
      <div class="row">
        <div class="col-12" id="device">
          <div class="row page-titles">
            <div class="col-md-5 col-8 align-self-center">

              <h3 class="text-themecolor">  <i class="fa fa-arrow-circle-o-right"></i> &nbsp {{$t('department.AllDepart')}}</h3>

            </div>
            <div class="col-md-7 col-4 align-self-center">
              <div class="d-flex m-t-10 justify-content-end">
                <div class="d-flex mr-2">
                  <button class="btn btn-primary" data-toggle="modal" data-target="#modealDepart"><i class="fa fa-plus-circle"></i> {{$t('department.AddDepart')}}  </button>
                </div>
              </div>
            </div>
          </div>
          <add-component></add-component>
          <div class="table-device">
            <table class="table table-bordered table-hover text-center">
              <thead>
                <tr>
                  <th scope="col">STT</th>
                  <th scope="col">{{$t('department.NameDepart')}}</th>
                  <th scope="col">{{$t('department.Symbol')}}</th>
                  <th>{{$t('common.Actions')}}</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(depart,index) in listDepart">
                  <th scope="row">{{index+1}}</th>
                  <td>{{depart.Name}}</td>
                  <td>{{depart.Symbol}}</td>
                  <td>
                    <button class="btn btn-warning" @click="ShowEditModel(depart)">
                      <i class="fa fa-edit"></i>
                      {{$t('common.Edit')}}
                    </button>
                    <button class="btn btn-danger" @click="ShowModelDelete(depart)">
                      <i class="fa fa-trash"></i>
                      {{$t('common.Delete')}}
                    </button>
                  </td>
                </tr>

              </tbody>
            </table>
          </div>

          <div class="modal fade " id="modealEdit" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title"> <i class="fa fa-television"></i>{{$t('homepage.Department_Home')}}</h5>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <div class="form-group row ">
                    <label class="col-sm-4 col-form-label">{{$t('department.NameDepart')}}</label>
                    <div class="col-sm-8">
                      <input type="text" v-model="nameDepart" class="form-control">
                    </div>
                  </div>
                  <div class="form-group row ">
                    <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('department.Symbol')}}</label>
                    <div class="col-sm-8">
                      <input type="text" v-model="nameSys" class="form-control">
                    </div>
                  </div>
                </div>
                <div class="modal-footer">
                  <button type="button" class="btn btn-danger" @click="Submit">{{$t('common.Edit')}}</button>
                  <button type="button" class="btn btn-secondary" data-dismiss="modal">{{$t('common.Close')}}</button>
                </div>
              </div>
            </div>
          </div>

          <div class="modal fade" id="modalDelete" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title" id="exampleModalLabel">{{$t('department.DeletDepart')}}</h5>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <p>
                    {{$t('department.DepartQuestion')}} :
                    <b>
                      {{nameDepart}}
                    </b>
                  </p>
                </div>
                <div class="modal-footer">
                  <button type="button" class="btn btn-danger" @click="DeleteDepart">{{$t('common.Yes')}}</button>
                  <button type="button" class="btn btn-secondary" data-dismiss="modal">{{$t('common.Close')}}</button>
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
  import { mapGetters } from 'vuex'
  import AddComponent from './AddDepartComponent'
  export default {
    components: {
      AddComponent
    },
    computed: {
      ...mapGetters(["listDepart"])
    },
    data() {
      return {
        nameDepart: '',
        nameSys: '',
        idDepart: 0
      }
    },
    methods: {
      GetListDepart() {
        this.$store.dispatch('getListDepart');
      },
      ShowModelDelete(depart) {
        $("#modalDelete").modal("show");
        this.nameDepart = depart.Name;
        this.idDepart = depart.Id;
      },
      ShowEditModel(depart) {
        $("#modealEdit").modal("show");
        this.idDepart = depart.Id;
        this.nameDepart = depart.Name;
        this.nameSys = depart.Symbol;
      },
      DeleteDepart() {
        axios.post('/Depart/DeleteDepart', { id: this.idDepart }).then(res => {
          NotifySuccess(this.langIndex("alert.Depart_Delete"))
          $("#modalDelete").modal("hide");
          this.$store.dispatch('getListDepart');
        })
      },
      Submit() {
        if (this.CheckData()) {
          axios.post('/Depart/UpdateDepart', { id: this.idDepart, name: this.nameDepart, sys: this.nameSys }).then(res => {
            NotifySuccess(this.langIndex("alert.Depart_Edit"))
            $("#modealEdit").modal("hide");
            this.$store.dispatch('getListDepart');
          }).catch(e => { console.log(e) })
        }

      },
 CheckData() {
        if (this.nameDepart == "") {
          NotifyDanger(this.langIndex("alert.Depart_Name"))
          return false;
        }
        else if (this.nameSys == "") {
          NotifyDanger(this.langIndex("alert.Depart_Symbol"));
          return false
        }
        else
          return true
      }
    },
    created() {
      FadeLoadingBar();
      this.GetListDepart();
    }
  }
</script>
