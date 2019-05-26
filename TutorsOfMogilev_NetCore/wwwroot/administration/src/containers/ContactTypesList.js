import { connect } from 'react-redux';
import { getSortedContactTypes } from './../selectors/contactTypes';
import contactTypeAC from '../AC/contactType';
import * as dialogsAC from '../AC/dialogs';
import EntityList from '../components/EntityList';

const mapStateToProps = state => {
  return {
    items: getSortedContactTypes(state),
    loading: state.contactTypes.loading,
    loaded: state.contactTypes.loaded,
    deleteDialogOpen: state.dialogs.deleteConfirmOpen,
    withInputDialogOpen: state.dialogs.withInputOpen
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadData: () => {
      dispatch(contactTypeAC.loadEntities());
    },
    remove: id => {
      dispatch(contactTypeAC.removeEntity(id));
    },
    create: contactType => {
      dispatch(contactTypeAC.createEntity(contactType));
    },
    update: (id, contactType) => {
      dispatch(contactTypeAC.updateEntity(id, contactType));
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
)(EntityList);
