import { connect } from 'react-redux';
import { getSortedDistricts } from './../selectors/districts';
import * as districtAC from '../AC/district';
import * as dialogsAC from '../AC/dialogs';
import EnityList from '../components/EnityList';

const mapStateToProps = state => {
  return {
    items: getSortedDistricts(state),
    loading: state.districts.loading,
    loaded: state.districts.loaded,
    deleteDialogOpen: state.dialogs.deleteConfirmOpen,
    withInputDialogOpen: state.dialogs.withInputOpen
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadData: () => {
      dispatch(districtAC.loadDistricts());
    },
    remove: id => {
      dispatch(districtAC.removeDistrict(id));
    },
    create: district => {
      dispatch(districtAC.createDistrict(district));
    },
    update: (id, district) => {
      dispatch(districtAC.updateDistrict(id, district));
    },
    openDeleteDialog: () => {
      dispatch(dialogsAC.openDeleteConfirmDialog());
    },
    closeDeleteDialog: () => {
      dispatch(dialogsAC.closeDeleteConfirmDialog());
    },
    openInputDialog: () => {
      dispatch(dialogsAC.openDialogWithInput());
    },
    closeInputDialog: () => {
      dispatch(dialogsAC.closeDialogWithInput());
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(EnityList);
