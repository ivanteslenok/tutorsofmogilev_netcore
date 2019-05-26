import { connect } from 'react-redux';
import { getSortedContactTypes } from './../selectors/contactTypes';
import contactTypeAC from '../AC/contactType';
import EntityList from '../components/EntityList';

const mapStateToProps = state => {
  return {
    items: getSortedContactTypes(state),
    loading: state.contactTypes.loading,
    loaded: state.contactTypes.loaded,
    loadingFailed: state.contactTypes.loadingFailed
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
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(EntityList);
