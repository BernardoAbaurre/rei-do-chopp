module.exports = function (plop) {
    const baseNameSpace = 'ReiDoChopp';


    plop.setHelper('namespace', () => baseNameSpace);

    plop.setHelper('camelCase', (text) => {
        if (!text || typeof text !== 'string') return '';
        return text.charAt(0).toLowerCase() + text.slice(1);
    });


    plop.setHelper('pascalCase', (text) => {
    if (!text || typeof text !== 'string') return '';
    return text
        .replace(/(^\w|_\w)/g, (m) => m.replace('_', '').toUpperCase());
    });

    plop.setHelper('ifEquals', function (arg1, arg2, options) {
        return arg1 === arg2 ? options.fn(this) : options.inverse(this);
    });

    plop.setHelper('isString', type => type === 'string' || type === 'String');
    plop.setHelper('isNumber', type =>
        ['int', 'Int32', 'long', 'double', 'float', 'decimal'].includes(type.replace('?', ''))
    );
    plop.setHelper('isDate', type =>
        ['DateTime', 'DateOnly', 'Date'].includes(type.replace('?', ''))
    );
    plop.setHelper('isNullable', type => type.includes('?'));
    plop.setHelper('isObject', type =>
        !['int', 'Int32', 'long', 'double', 'float', 'decimal', 'string', 'String', 'DateTime', 'Date', 'DateOnly'].includes(type.replace('?', ''))
    );

    plop.setGenerator('entity', {
        description: 'Generates a new entity with service, repository, controller, request, response, profile, and appService',
        prompts: [
            {
                type: 'input',
                name: 'name',
                message: 'Entity name (singular):'
            },
            {
                type: 'input',
                name: 'pluralName',
                message: 'Entity name in plural:'
            },
            {
                type: 'confirm',
                name: 'addProperties',
                message: 'Do you want to add properties?',
                default: true
            },
            {
                type: 'input',
                name: 'properties',
                message: 'Add properties (format: name:type, separate multiple with commas):',
                when: (answers) => answers.addProperties
            }
        ],
        actions: (data) => {
            // Process properties into structured objects
            if (data.properties) {
                const props = data.properties.split(',').map((prop) => {
                    const [propertyName, propertyType] = prop.split(':');
                    return {
                        propertyName: propertyName.trim(),
                        propertyType: propertyType.trim()
                    };
                });
                data.properties = props;
            }

            // Define actions
            const actions = [
                // ========================= DOMAIN =======================
                {
                    type: 'add',
                    path: baseNameSpace + '.Domain/{{pascalCase pluralName}}/Entities/{{pascalCase name}}.cs',
                    templateFile: 'Templates/Entity.hbs'
                },
                {
                    type: 'add',
                    path: baseNameSpace + '.Domain/{{pascalCase pluralName}}/Services/{{pascalCase name}}Service.cs',
                    templateFile: 'Templates/Service.hbs'
                },
                {
                    type: 'add',
                    path: baseNameSpace + '.Domain/{{pascalCase pluralName}}/Services/Interfaces/I{{pascalCase name}}Service.cs',
                    templateFile: 'Templates/IService.hbs'
                },
                {
                    type: 'add',
                    path: baseNameSpace + '.Domain/{{pascalCase pluralName}}/Repositories/I{{pascalCase name}}Repository.cs',
                    templateFile: 'Templates/IRepository.hbs'
                },
                {
                    type: 'add',
                    path: baseNameSpace + '.Domain/{{pascalCase pluralName}}/Repositories/Filters/{{pascalCase pluralName}}ListFilter.cs',
                    templateFile: 'Templates/Filter.hbs'
                },

                // ======================== APPLICATION ==================
                {
                    type: 'add',
                    path: baseNameSpace + '.Application/{{pascalCase pluralName}}/Profiles/{{pascalCase name}}Profile.cs',
                    templateFile: 'Templates/Profile.hbs'
                },
                {
                    type: 'add',
                    path: baseNameSpace + '.Application/{{pascalCase pluralName}}/Services/Interfaces/I{{pascalCase name}}AppService.cs',
                    templateFile: 'Templates/IAppService.hbs'
                },
                {
                    type: 'add',
                    path: baseNameSpace + '.Application/{{pascalCase pluralName}}/Services/{{pascalCase name}}AppService.cs',
                    templateFile: 'Templates/AppService.hbs'
                },
                // ====================== DATATRANSFER =================
                {
                    type: 'add',
                    path: baseNameSpace + '.DataTransfer/{{pascalCase pluralName}}/Requests/{{pascalCase name}}EditRequest.cs',
                    templateFile: 'Templates/EditRequest.hbs'
                },
                {
                    type: 'add',
                    path: baseNameSpace + '.DataTransfer/{{pascalCase pluralName}}/Requests/{{pascalCase name}}ListRequest.cs',
                    templateFile: 'Templates/ListRequest.hbs'
                },
                {
                    type: 'add',
                    path: baseNameSpace + '.DataTransfer/{{pascalCase pluralName}}/Requests/{{pascalCase name}}InsertRequest.cs',
                    templateFile: 'Templates/InsertRequest.hbs'
                },
                {
                    type: 'add',
                    path: baseNameSpace + '.DataTransfer/{{pascalCase pluralName}}/Responses/{{pascalCase name}}Response.cs',
                    templateFile: 'Templates/Response.hbs'
                },
                // ===================== CONTROLLER ====================
                {
                    type: 'add',
                    path: baseNameSpace + '.Api/Controllers/{{pascalCase pluralName}}/{{pascalCase name}}Controllers.cs',
                    templateFile: 'Templates/Controller.hbs'
                },
                // ===================== REPOSITORY ====================
                {
                    type: 'add',
                    path: baseNameSpace + '.Infra/{{pascalCase pluralName}}/Repositories/{{pascalCase name}}Repository.cs',
                    templateFile: 'Templates/Repository.hbs'
                }
            ];
            
            console.log('DATA DEBUG:', data);

            return actions;
        }
    });
};
