import { connect } from 'react-redux';
import * as tutorAC from '../AC/tutor';
import DataGrid from '../components/DataGrid';

const mapStateToProps = state => {
  return {
    items: state.tutors.items,
    totalCount: state.tutors.totalCount,
    loading: state.tutors.loading,
    gridColumns: state.tutors.gridColumns
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadData: filter => {
      dispatch(tutorAC.loadTutors(filter));
    }
    // remove: id => {
    //   dispatch(districtAC.removeDistrict(id));
    // },
    // create: district => {
    //   dispatch(districtAC.createDistrict(district));
    // },
    // update: (id, district) => {
    //   dispatch(districtAC.updateDistrict(id, district));
    // },
    // openDeleteDialog: () => {
    //   dispatch(dialogsAC.openDeleteConfirmDialog());
    // },
    // closeDeleteDialog: () => {
    //   dispatch(dialogsAC.closeDeleteConfirmDialog());
    // }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(DataGrid);
