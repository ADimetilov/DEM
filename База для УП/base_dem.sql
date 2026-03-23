BEGIN;
CREATE TABLE public."Role"
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY,
    "role" character varying(50),
    PRIMARY KEY (id)
);
ALTER TABLE if EXISTS public."Role"
    OWNER to postgres;

CREATE TABLE public."Category"
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY,
    category character varying(50),
    PRIMARY KEY (id)
);
ALTER TABLE if EXISTS public."Category"
    OWNER to postgres;

CREATE TABLE public."Manufacter"(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY,
    manufacter character varying(50),
    PRIMARY KEY (id)
);
ALTER TABLE if EXISTS public."Manufacter"
    OWNER to postgres;

CREATE TABLE public."Supplier"(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY,
    supplier character varying(50),
    PRIMARY KEY (id)
);
ALTER TABLE if EXISTS public."Supplier"
    OWNER to postgres;

CREATE TABLE public."Unit"(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY,
    unit character varying(50),
    PRIMARY KEY (id)
);
ALTER TABLE if EXISTS public."Unit"
    OWNER to postgres;

CREATE TABLE public."User"
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY,
    fio character varying(200),
    "login" character varying(100),
    "password" character varying(100),
    role_id integer NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (role_id)
        REFERENCES public."Role" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
);
ALTER TABLE IF EXISTS public."User"
    OWNER to postgres;

CREATE TABLE public."Status"(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY,
    "status" character varying(50),
    PRIMARY KEY (id)
);
ALTER TABLE if EXISTS public."Status"
    OWNER to postgres;

CREATE TABLE public."Product"
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY,
    category_id integer,
    "name" character varying(200),
    "desc" character varying(200),
    man_id integer,
    supplier_id integer,
    cost integer,
    unit_id integer,
    score integer,
    sale integer,
    path_photo character varying(200),
    PRIMARY KEY (id),
    FOREIGN KEY (category_id)
        REFERENCES public."Category" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID,
    FOREIGN KEY (man_id)
        REFERENCES public."Manufacter" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID,
    FOREIGN KEY (supplier_id)
        REFERENCES public."Supplier" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID,
    FOREIGN KEY (unit_id)
        REFERENCES public."Unit" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
);
ALTER TABLE IF EXISTS public."Product"
    OWNER to postgres;

CREATE TABLE public."Order"(
    id integer not NULL GENERATED ALWAYS as IDENTITY,
    art integer,
    status_id integer,
    product_id integer,
    adres character varying(200),
    date_start date,
    date_end date,
    PRIMARY KEY(id),
    FOREIGN KEY (status_id)
        REFERENCES public."Status" (id) MATCH SIMPLE
        on UPDATE no ACTION
        on DELETE no ACTION
        not VALID,
    FOREIGN KEY (product_id)
        REFERENCES public."Product" (id) MATCH SIMPLE
        on UPDATE no ACTION
        on DELETE no ACTION
        not VALID
);
ALTER TABLE if EXISTS public."Order"
    OWNER to postgres;


INSERT INTO public."Category" ("category") VALUES ('Спортивная');
INSERT INTO public."Category" ("category") VALUES ('Повседневная');
INSERT INTO public."Category" ("category") VALUES ('Деловая');
INSERT INTO public."Category" ("category") VALUES ('Для активного отдыха');
INSERT INTO public."Category" ("category") VALUES ('Специальная');

INSERT INTO public."Manufacter" ("manufacter") VALUES ('Kari');
INSERT INTO public."Manufacter" ("manufacter") VALUES ('Puma');
INSERT INTO public."Manufacter" ("manufacter") VALUES ('Adidas');

INSERT INTO public."Supplier" ("supplier") VALUES ('Man-armory');
INSERT INTO public."Supplier" ("supplier") VALUES ('WMB');
INSERT INTO public."Supplier" ("supplier") VALUES ('Serm');

INSERT INTO public."Unit" ("unit") VALUES ('Шт.');
INSERT INTO public."Unit" ("unit") VALUES ('Кор.');
INSERT INTO public."Unit" ("unit") VALUES ('Парт.');

INSERT INTO public."Product" ("category_id","name","desc","man_id","supplier_id","cost","unit_id","score"
,"sale","path_photo")
    VALUES(1,'Кроссовки AirUnix', 'Удобство, комфорт',3,2,5000,1,10,25,'None');
INSERT INTO public."Product" ("category_id","name","desc","man_id","supplier_id","cost","unit_id","score"
,"sale","path_photo")
    VALUES(4,'Ботинки MaxArmor', 'Протектор просто вау',2,1,10000,2,10,0,'None');
INSERT INTO public."Product" ("category_id","name","desc","man_id","supplier_id","cost","unit_id","score"
,"sale","path_photo")
    VALUES(3,'Туфли AlfaMen', 'Для самых деловых случаев',3,2,2500,1,0,75,'None');

INSERT INTO public."Status" ("status") VALUES ('Сборка');
INSERT INTO public."Status" ("status") VALUES ('Доставлен в пункт выдачи');
INSERT INTO public."Status" ("status") VALUES ('Получен');

INSERT INTO public."Order" ("art","status_id","product_id","adres","date_start","date_end")
VALUES (11002,1,2,'Ул. Юрина 210а','2026-03-20','2026-03-23');
COMMIT;