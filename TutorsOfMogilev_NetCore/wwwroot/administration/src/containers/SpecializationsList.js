import { connect } from 'react-redux';
import { getSortedSpecializations } from './../selectors/specializations';
import specializationAC from '../AC/specialization';
import EntityList from '../components/EntityList';

const mapStateToProps = state => {
  return {
    items: getSortedSpecializations(state),
    loading: state.specializations.loading,
    loaded: state.specializations.loaded,
    loadingFailed: state.specializations.loadingFailed
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadData: () => {
      dispatch(specializationAC.loadEntities());
    },
    remove: id => {
      dispatch(specializationAC.removeEntity(id));
    },
    create: specialization => {
      dispatch(specializationAC.createEntity(specialization));
    },
    update: (id, specialization) => {
      dispatch(specializationAC.updateEntity(id, specialization));
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(EntityList);
