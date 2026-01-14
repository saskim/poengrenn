import { environment } from '../../../environments/environment';

export const Endpoints = {
    User: {
        Login:                      environment.restApiUrl + 'bruker/login'
    },
    Competition: {
        GetAll:                     environment.restApiUrl + 'konkurranse/',
        GetOpen:                    environment.restApiUrl + 'konkurranse/open',
        GetDone:                    environment.restApiUrl + 'konkurranse/done',
        GetById:                    environment.restApiUrl + 'konkurranse/{id}',
        Post:                       environment.restApiUrl + 'konkurranse/',
        Put:                        environment.restApiUrl + 'konkurranse/',
        Delete:                     environment.restApiUrl + 'konkurranse/{id}'
    },
    CompetitionParticipant: {
        GetAll:                     environment.restApiUrl + 'konkurranse/{id}/deltaker/',
        Post:                       environment.restApiUrl + 'konkurranse/{id}/deltaker/',
        Put:                        environment.restApiUrl + 'konkurranse/{id}/deltaker/',
        Delete:                     environment.restApiUrl + 'konkurranse/{id}/deltaker/{deltakerId}/klasse/{klasseId}'
    },
    CompetitionType: {
        GetAll:                     environment.restApiUrl + 'konkurransetype/',
    },
    CompetitionClass: {
        GetAll:                     environment.restApiUrl + 'konkurranseklasse/',
        GetByTypeID:                environment.restApiUrl + 'konkurranseklasse/type/{typeId}',
        Post:                       environment.restApiUrl + 'konkurranseklasse/',
        Put:                        environment.restApiUrl + 'konkurranseklasse/',
        Delete:                     environment.restApiUrl + 'konkurranseklasse/{id}'
    },
    Person: {
        GetAll:                     environment.restApiUrl + 'person/',
        GetById:                    environment.restApiUrl + 'person/{id}',
        Post:                       environment.restApiUrl + 'person/',
        Put:                        environment.restApiUrl + 'person/',
    }
}
