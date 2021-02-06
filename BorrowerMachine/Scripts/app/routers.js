//Define router to component
import HomeComponent from './Home/HomeComponent'
import DeviceComponent from './Device/DeviceComponent'
import HistoryComponent from './History/HistoryComponent'
import DepartComponent from './Department/DepartmentComponent'

export const routes = [
    {
        path: '/',
        name: 'Home',
        meta: { title: 'Trang chủ' },
        component: HomeComponent
    },
    {
        path: '/thiet-bi',
        name: 'Device',
        meta: { title: 'Quản lý thiết bị' },
        component: DeviceComponent
    },
    {
        path: '/lich-su',
        name: 'History',
        meta: { title: 'Lịch sử mượn máy' },
        component: HistoryComponent
    },
    {
        path: '/bo-phan',
        name: 'Department',
        meta: { title: 'Quản lý bộ phận' },
        component: DepartComponent
    }
    //{
    //  path: "*",
    //  component: PageNotFound,
    //  meta: { title: '404 Page! Không tìm thấy trang' },
    //}
];
