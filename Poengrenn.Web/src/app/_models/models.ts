
export class Konkurranse {
    konkurranseID: number;
    serie: string;
    navn: string;
    dato: Date;
    typeID: string;
    avlyst: boolean;
    konkurranseDeltakere: KonkurranseDeltaker[];
    konkurranseKlasser: KonkurranseKlasse[];
    konkurranseType: KonkurranseType;
}

export class KonkurranseKlasse {
    klasseID: string;
    typeID: string;
    navn: string;
    minAlder: number;
    maxAlder: number;
    kjonn: string;
    forsteStartnummer: number;
    sisteStartnummer: number;
    medTidtaking: boolean;
    distanseKm: number;
    konkurranseType: KonkurranseType;
}

export class KonkurranseDeltaker {
    konkurranseID: number;
    klasseID: string;
    personID: number;
    startNummer: number;
    startTid: Date;
    sluttTid: Date;
    tidsforbruk: Date;
    konkurranse: Konkurranse;
    konkurranseKlasse: KonkurranseKlasse;
    person: Person;
}
export class NyKonkurranseDeltaker {
    personID: number;
    klasseID: string;
}

export class KonkurranseOpprett {
    typeID: string;
    navn: string;
    datoer: Date[];
}

export class KonkurranseType {
    typeID: string;
    navn: string;
    standardAntallKonkurranser:  number;
    aktiv: boolean;
}

export class Person {
    personID: number;
    fornavn: string;
    etternavn: string;
    fodselsar: number
    kjonn: string;
    epost: string;
    telefon: string;
}


